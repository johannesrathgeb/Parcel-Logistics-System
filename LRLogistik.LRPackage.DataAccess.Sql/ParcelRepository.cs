using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using LRLogistik.LRPackage.Services.DTOs;
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
        public Entities.Parcel Create(Entities.Parcel parcel)
        {
            _logger.LogInformation($"Creating parcel in DB: {JsonConvert.SerializeObject(parcel)}");
            try
            {
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
                return _dbContext.Parcels.Single(p => p.TrackingId == trackingid);
            }
            catch(InvalidOperationException e)
            {
                _logger.LogError($"Getting Parcel was invalid");
                throw new Entities.Exceptions.DataAccessNotFoundException("GetByTrackingId", "Parcel with Id " + trackingid + " not found", e);
            }
        }

        public Entities.Parcel Update(Entities.Parcel p)
        {
            throw new NotImplementedException();
        }
    }
}
