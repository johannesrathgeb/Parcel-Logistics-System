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
        SampleContext _dbContext;

        public WarehouseRepository(SampleContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WarehouseRepository()
        {
            _dbContext = new SampleContext();
        }

        public object Create(Warehouse w)
        {
            if(w == null)
            {
                return new Entities.Error() { ErrorMessage = "string" };
            }
            _dbContext.Warehouses.Add(w);
            _dbContext.SaveChanges();
            return w; 
        }

        public void Delete(string id)
        {
            Entities.Warehouse warehouse = _dbContext.Warehouses.SingleOrDefault(w => w.HopId == id);

            if (warehouse != null)
            {
                _dbContext.Remove(warehouse);
                _dbContext.SaveChanges();
            }
        }

        public object GetByHopId(string id)
        {
            Entities.Warehouse warehouse = _dbContext.Warehouses.SingleOrDefault(w => w.HopId == id);

            if (warehouse == null)
            {
                return new Entities.Error() { ErrorMessage = "string" };
            }
            return warehouse;
        }

        public Warehouse Update(Warehouse w)
        {
            throw new NotImplementedException();
        }
    }
}
