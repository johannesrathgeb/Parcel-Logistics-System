using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var parent =
                    _dbContext.Hops.OfType<Warehouse>()
                        .Include(x => x.NextHops)
                        .ThenInclude(x => x.Hop)
                        .AsEnumerable()
                        .SingleOrDefault(x => x.NextHops.Any(y => y.Hop.HopId == hop.HopId));

            return parent;
        }

        public List<HopArrival> Routing(Hop hopA, Hop hopB)
        {
            // Get the parent of hop A and B
            var aParent = GetParent(hopA);
            var bParent = GetParent(hopB);

            // Are the parent the SAME HOP?
            if (aParent == bParent)
            {
                // -- YES.. we found the common hop and are done
                var commonParent = aParent;
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
                var route = Routing(aParent, bParent);

                var aParentArrival = new HopArrival()
                {
                    HopArrivalId = aParent.HopId,
                    Code = aParent.Code,
                    Description = aParent.Description,
                    DateTime = DateTime.Now

                };

                var bParentArrival = new HopArrival()
                {
                    HopArrivalId = bParent.HopId,
                    Code = bParent.Code,
                    Description = bParent.Description,
                    DateTime = DateTime.Now
                };

                route.Insert(0, aParentArrival);
                route.Add(bParentArrival);

                return route;
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
            catch(InvalidOperationException e)
            {
                _logger.LogError($"Getting Parcel was invalid");
                throw new Entities.Exceptions.DataAccessNotFoundException("GetByTrackingId", "Parcel with Id " + trackingid + " not found", e);
            }
        }

        public void UpdateDeliveryState(string trackingid)
        {
            (from p in _dbContext.Parcels
             where p.TrackingId == trackingid
             select p).ToList()
             .ForEach(x => x.State = Parcel.StateEnum.DeliveredEnum);
            _dbContext.SaveChanges();
        }

        public void ReportHop(string trackingId, string code)
        {
            Parcel parcel = GetByTrackingId(trackingId);
            Hop hop = _warehouseRepository.GetByHopCode(code);
            var hopArrival = new HopArrival()
            {
                HopArrivalId = hop.HopId,
                Code = hop.Code,
                Description = hop.Description,
                DateTime = DateTime.Now
            };

            //foreach (HopArrival h in parcel.FutureHops) 
            //{
            //    if(h.Code== hop.Code)
            //    {
            //        tempHop = h;
            //        break;
            //    }
            //}
            parcel.FutureHops.RemoveAt(0);
            parcel.VisitedHops.Add(hopArrival);

            if (hop.HopType == "warehouse")
            {
                parcel.State = Parcel.StateEnum.InTransportEnum;
            }
            else if (hop.HopType == "truck")
            {
                parcel.State = Parcel.StateEnum.InTruckDeliveryEnum;
            }
            else
            {
                //TODO TRANSFER warehouse iwos
            }
            //(from p in _dbContext.Parcels
            //    where p.TrackingId == parcel.TrackingId
            //    select p).ToList()
            //    .ForEach(x => x.State = parcel.State);
            //(from p in _dbContext.Parcels
            // where p.TrackingId == parcel.TrackingId
            // select p).ToList()
            //    .ForEach(x => x.VisitedHops = parcel.VisitedHops);
            //(from p in _dbContext.Parcels
            // where p.TrackingId == parcel.TrackingId
            // select p).ToList()
            //    .ForEach(x => x.FutureHops = parcel.FutureHops);

            Delete(trackingId);
            _dbContext.Parcels.Add(parcel);
            _dbContext.SaveChanges();
        }
    }
}
