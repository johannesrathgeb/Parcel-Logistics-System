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
        SampleContext _dbContext = new SampleContext();


        [HttpPost]
        public void Create(Parcel parcel)
        {
            _dbContext.Parcels.Add(parcel);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Parcel GetByTrackingId(string trackingid)
        {
            return _dbContext.Parcels.Single(p => p.TrackingId == trackingid);
        }

        public IEnumerable<Parcel> GetByXX(string xx)
        {
            throw new NotImplementedException();
        }

        public Parcel GetByYY(int yy)
        {
            throw new NotImplementedException();
        }

        public Parcel Update(Parcel p)
        {
            throw new NotImplementedException();
        }
    }
}
