using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly ILogger _logger;
        public WarehouseRepository(SampleContext context, ILogger<WarehouseRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public object Create(Warehouse w)
        {
            _logger.LogInformation($"Creating Warehouse in DB: {JsonConvert.SerializeObject(w)}");
            if (w == null)
            {
                _logger.LogDebug($"Warehouse Creation was invalid");
                return new Entities.Error() { ErrorMessage = "string" };
            }
            _dbContext.Warehouses.Add(w);
            _dbContext.SaveChanges();
            _logger.LogInformation($"Warehouse successfully created: {JsonConvert.SerializeObject(w)}");
            return w; 
        }

        public void Delete(string id)
        {
            Entities.Warehouse warehouse = _dbContext.Warehouses.SingleOrDefault(w => w.HopId == id);
            _logger.LogInformation($"Deleting warehouse from DB: {JsonConvert.SerializeObject(warehouse)}");
            if (warehouse != null)
            {
                _dbContext.Remove(warehouse);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Warehouse deleted: {JsonConvert.SerializeObject(warehouse)}");
            }
        }

        public object GetByHopId(string id)
        {
            _logger.LogInformation($"Getting Warehouse from DB: {JsonConvert.SerializeObject(id)}");
            Entities.Warehouse warehouse = _dbContext.Warehouses.SingleOrDefault(w => w.HopId == id);
            _logger.LogInformation($"Warehouse found in DB: {JsonConvert.SerializeObject(warehouse)}");
            if (warehouse == null)
            {
                _logger.LogDebug($"Getting Warehouse was invalid");
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
