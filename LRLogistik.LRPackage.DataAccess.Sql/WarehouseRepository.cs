using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LRLogistik.LRPackage.DataAccess.Sql
{
    public class WarehouseRepository : IWarehouseRepository
    {
        SampleContext _dbContext = new SampleContext();

        public void Create(Warehouse w)
        {
            _dbContext.Warehouses.Add(w);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Warehouse GetByTrackingId(string trackingid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Warehouse> GetByXX(string xx)
        {
            throw new NotImplementedException();
        }

        public Warehouse GetByYY(int yy)
        {
            throw new NotImplementedException();
        }

        public Warehouse Update(Warehouse w)
        {
            throw new NotImplementedException();
        }
    }
}
