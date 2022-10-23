using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Sql
{
    public class ParcelRepository : IParcelRepository
    {
        public Parcel Create(Parcel p)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Parcel GetByTrackingId(string trackingid)
        {
            throw new NotImplementedException();
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
