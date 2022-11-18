using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
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

        public ParcelRepository(SampleContext context, ILogger<ParcelRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        [HttpPost]
        public object Create(Entities.Parcel parcel)
        {
            _logger.LogInformation($"Creating parcel in DB: {JsonConvert.SerializeObject(parcel)}");
            if (parcel == null)
            {
                _logger.LogDebug($"Parcel Creation was invalid");
                return new Entities.Error() { ErrorMessage = "string" };
            }
            _dbContext.Parcels.Add(parcel);
            _dbContext.SaveChanges();
            _logger.LogInformation($"Parcel successfully created: {JsonConvert.SerializeObject(parcel)}");
            return new Entities.Parcel { TrackingId = parcel.TrackingId };
        }

        public void Delete(string TrackingId)
        {
            Entities.Parcel parcel = _dbContext.Parcels.SingleOrDefault(p => p.TrackingId == TrackingId);
            _logger.LogInformation($"Deleting parcel from DB: {JsonConvert.SerializeObject(parcel)}");
            if (parcel != null)
            {
                _dbContext.Remove(parcel);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Parcel deleted: {JsonConvert.SerializeObject(parcel)}");
            }
        }

        public object GetByTrackingId(string trackingid)
        {
            _logger.LogInformation($"Getting Parcel from DB: {JsonConvert.SerializeObject(trackingid)}");
            Entities.Parcel parcel = _dbContext.Parcels.SingleOrDefault(p => p.TrackingId == trackingid);
            _logger.LogInformation($"Parcel found in DB: {JsonConvert.SerializeObject(parcel)}");
            if (parcel == null)
            {
                _logger.LogDebug($"Getting Parcel was invalid");
                return new Entities.Error() { ErrorMessage = "string" };
            }
            return parcel;
        }

        public Entities.Parcel Update(Entities.Parcel p)
        {
            throw new NotImplementedException();
        }
    }
}
