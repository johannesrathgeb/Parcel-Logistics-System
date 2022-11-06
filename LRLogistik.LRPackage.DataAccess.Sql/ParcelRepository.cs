using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public ParcelRepository()
        {
            _dbContext = new SampleContext();
        }

        public ParcelRepository(SampleContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        public object Create(Entities.Parcel parcel)
        {
            if(parcel == null)
            {
                return new Entities.Error() { ErrorMessage = "string" };
            }
            _dbContext.Parcels.Add(parcel);
            _dbContext.SaveChanges();

            return new Entities.Parcel { TrackingId = parcel.TrackingId };
        }

        public void Delete(string TrackingId)
        {
            Entities.Parcel parcel = _dbContext.Parcels.SingleOrDefault(p => p.TrackingId == TrackingId);

            if(parcel != null)
            {
                _dbContext.Remove(parcel);
                _dbContext.SaveChanges();
            }
        }

        public object GetByTrackingId(string trackingid)
        {
            Entities.Parcel parcel = _dbContext.Parcels.SingleOrDefault(p => p.TrackingId == trackingid);

            if(parcel == null)
            {
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
