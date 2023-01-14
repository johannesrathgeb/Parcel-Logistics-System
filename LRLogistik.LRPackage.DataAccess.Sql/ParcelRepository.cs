using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System.Net.Http; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRLogistik.LRPackage.DataAccess.Entities.Exceptions;
using LRLogistik.LRPackage.ServiceAgents.Interfaces;

namespace LRLogistik.LRPackage.DataAccess.Sql
{
    public class ParcelRepository : IParcelRepository
    {
        SampleContext _dbContext;
        private readonly ILogger _logger;
        IWarehouseRepository _warehouseRepository;

        public ParcelRepository(SampleContext context, ILogger<ParcelRepository> logger, IWarehouseRepository warehouseRepository)
        {
            _dbContext = context;
            _logger = logger;
            _warehouseRepository = warehouseRepository;
        }

        public Warehouse GetParent(Hop hop)
        {
            try
            {
                var parent =
                    _dbContext.Hops.OfType<Warehouse>()
                        .Include(x => x.NextHops)
                        .ThenInclude(x => x.Hop)
                        .AsEnumerable()
                        .SingleOrDefault(x => x.NextHops.Any(y => y.Hop.HopId == hop.HopId));
                _logger.LogInformation($"Parent Hop found!");
                return parent;
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Getting Parent was unsuccesful");
                throw new Entities.Exceptions.DataAccessNotFoundException("GetParent", "Parent of Hop " + hop + " not found", e);
            }
        }

        public List<HopArrival> Routing(Hop hop1, Hop hop2)
        {
            try
            {
                var parent1 = GetParent(hop1);
                var parent2 = GetParent(hop2);

                if (parent1 == parent2)
                {
                    var commonParent = parent1;
                    var HopArrival = new HopArrival()
                    {
                        Code = commonParent.Code,
                        Description = commonParent.Description,
                        DateTime = DateTime.Now
                    };

                    return new List<HopArrival>() { HopArrival };
                }
                else
                {
                    var route = Routing(parent1, parent2);

                    var parent1Arrival = new HopArrival()
                    {
                        Code = parent1.Code,
                        Description = parent1.Description,
                        DateTime = DateTime.Now

                    };

                    var parent2Arrival = new HopArrival()
                    {
                        Code = parent2.Code,
                        Description = parent2.Description,
                        DateTime = DateTime.Now
                    };

                    route.Insert(0, parent1Arrival);
                    route.Add(parent2Arrival);
                    _logger.LogInformation($"Route found!");
                    return route;
                }
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Routing was unsuccesful");
                throw new Entities.Exceptions.DataAccessNotFoundException("Routing", "Routing between " + hop1 + " and " + hop2 + " not found", e);
            }
            catch (DataAccessNotFoundException e)
            {
                _logger.LogError($"Routing was unsuccesful");
                throw new Entities.Exceptions.DataAccessNotFoundException("Routing", "Routing between " + hop1 + " and " + hop2 + " not found", e);
            }
        }

        [HttpPost]
        public Entities.Parcel Create(Entities.Parcel parcel, Point senderPoint, Point recipientPoint)
        {
            _dbContext.Database.EnsureCreated();
            _logger.LogInformation($"Creating parcel in DB: {JsonConvert.SerializeObject(parcel)}");
            try
            {

                //var senderCoordinates = _mapper.Map<Point>(_encodingagent.EncodeAddress(parcel.Sender)); 
                //var recipientCoordinates = _mapper.Map<Point>(_encodingagent.EncodeAddress(parcel.Recipient));


                var senderTruck = _dbContext.Hops.OfType<Truck>()
                    .AsEnumerable()
                    .SingleOrDefault(w => w.Region.Contains(senderPoint));

                var receiverTruck = _dbContext.Hops.OfType<Truck>()
                    .AsEnumerable()
                    .SingleOrDefault(w => w.Region.Contains(recipientPoint));

                parcel.FutureHops = Routing(senderTruck, receiverTruck);
                
                parcel.FutureHops.Insert(0, new HopArrival()
                {
                    Code = senderTruck.Code,
                    Description = senderTruck.Description,
                    DateTime = DateTime.Now
                });
                parcel.FutureHops.Add(new HopArrival()
                {
                    Code = receiverTruck.Code,
                    Description = receiverTruck.Description,
                    DateTime = DateTime.Now
                });

                _dbContext.Parcels.Add(parcel);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Parcel successfully created: {JsonConvert.SerializeObject(parcel)}");

                return new Entities.Parcel { TrackingId = parcel.TrackingId };
            }
            catch(ArgumentNullException e)
            {
                _logger.LogError($"Parcel Creation was invalid");
                throw new Entities.Exceptions.DataAccessNotCreatedException("Create", "Parcel was not created", e);
            }
        }

        public void Delete(string TrackingId)
        {
            Entities.Parcel parcel = GetByTrackingId(TrackingId);
            _logger.LogInformation($"Deleting parcel from DB: {JsonConvert.SerializeObject(parcel)}");
            try
            {
                _dbContext.Remove(parcel);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Parcel deleted: {JsonConvert.SerializeObject(parcel)}");
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Deleting Parcel was invalid");
                throw new Entities.Exceptions.DataAccessNotFoundException("Delete", "Parcel with Id " + TrackingId + " was not deleted", e);
            }
        }

        public Entities.Parcel GetByTrackingId(string trackingid)
        {
            _logger.LogInformation($"Getting Parcel from DB: {JsonConvert.SerializeObject(trackingid)}");
            try
            {
                return _dbContext.Parcels
                    .Include(x => x.VisitedHops)
                    .Include(x => x.FutureHops)
                    .Single(p => p.TrackingId == trackingid);
                    
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Getting Parcel was invalid");
                throw new Entities.Exceptions.DataAccessNotFoundException("GetByTrackingId", "Parcel with Id " + trackingid + " not found", e);
            }
        }

        public void UpdateDeliveryState(string trackingid)
        {
            try
            {
                (from p in _dbContext.Parcels
                 where p.TrackingId == trackingid
                 select p).ToList()
                             .ForEach(x => x.State = Parcel.StateEnum.DeliveredEnum);
                _dbContext.SaveChanges();
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Updating Delivery State was unsuccesful!");
                throw new Entities.Exceptions.DataAccessNotFoundException("UpdateDeliveryState", "Parcel with Id " + trackingid + " not found", e);
            }
        }

        public async void ReportHop(string trackingId, string code)
        {
            try
            {
                _dbContext.Database.EnsureCreated();
                HttpClient client = new HttpClient();
                Parcel parcel = GetByTrackingId(trackingId);
                Hop hop = _warehouseRepository.GetByHopCode(code);
                _logger.LogInformation($"Parcel with id {parcel.ParcelId} and Hop with id {hop.HopId} found.");
                var hopArrival = new HopArrival()
                {
                    //HopArrivalId = hop.HopId,
                    Code = hop.Code,
                    Description = hop.Description,
                    DateTime = DateTime.Now
                };

                parcel.FutureHops.RemoveAll(x => x.Code == code);
                parcel.VisitedHops.Add(hopArrival);

                _logger.LogInformation($"Parcel FutureHops removed and Visited Hops added!");

                if (hop.HopType == "warehouse")
                {
                    parcel.State = Parcel.StateEnum.InTransportEnum;
                    _logger.LogInformation($"Parcel State set to InTransport!");
                }
                else if (hop.HopType == "truck")
                {
                    parcel.State = Parcel.StateEnum.InTruckDeliveryEnum;
                    _logger.LogInformation($"Parcel State set to InTruckDelivery!");
                }
                else if (hop.HopType == "transferwarehouse")
                {
                    parcel.State = Parcel.StateEnum.TransferredEnum;
                    _logger.LogInformation($"Parcel State set to Transferred!");
                }
                else
                {
                    //TODO TRANSFER warehouse iwos
                }

                bool saveFailed;
                do
                {
                    saveFailed = false;
                    try
                    {
                        _dbContext.Parcels.Update(parcel);
                        _dbContext.SaveChanges();
                        _logger.LogInformation($"Saved to DB!");
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;
                        _logger.LogInformation($"Unable to save to DB! Retrying!");
                        // Update the values of the entity that failed to save from the store
                        ex.Entries.Single().Reload();
                    }
                } while (saveFailed);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Reporting Hop was unsuccesful!");
                throw new Entities.Exceptions.DataAccessNotFoundException("ReportHop", "Parcel with Id " + trackingId + " and Hop with Code " + code + " not found", e);
            }
        }

    }
}
