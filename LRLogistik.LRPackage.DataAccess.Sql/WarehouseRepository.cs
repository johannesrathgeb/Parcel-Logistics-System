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
        private static Random random = new Random();
        SampleContext _dbContext;
        private readonly ILogger _logger;
        public WarehouseRepository(SampleContext context, ILogger<WarehouseRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public object Create(Warehouse w)
        {
            if (w == null)
            {
                _logger.LogDebug($"Warehouse Creation was invalid");
                return new Entities.Error() { ErrorMessage = "string" };
            }
            _logger.LogInformation($"Creating Warehouse in DB: {JsonConvert.SerializeObject(w.HopId)}");
            _dbContext.Hops.Add(w);
            _dbContext.SaveChanges();
            _logger.LogInformation($"Warehouse successfully created: {JsonConvert.SerializeObject(w.HopId)}");
            return w; 
        }

        public void Delete(string id)
        {
            Entities.Warehouse warehouse = (Warehouse) _dbContext.Hops.SingleOrDefault(w => w.HopId == id);
            if (warehouse == null)
            {
                _logger.LogDebug($"Warehouse Deletion was invalid; Warehouse doesn't exist");
                return; 
            }
            _logger.LogInformation($"Deleting warehouse from DB: {JsonConvert.SerializeObject(warehouse.HopId)}");
            _dbContext.Remove(warehouse);
            _dbContext.SaveChanges();
            _logger.LogInformation($"Warehouse deleted");
        }

        public object GetByHopId(string id)
        {
            _logger.LogInformation($"Getting Warehouse from DB: {JsonConvert.SerializeObject(id)}");
            Entities.Warehouse warehouse = (Warehouse) _dbContext.Hops.SingleOrDefault(w => w.HopId == id);
            if (warehouse == null)
            {
                _logger.LogDebug($"Getting Warehouse was invalid");
                return new Entities.Error() { ErrorMessage = "string" };
            }
            _logger.LogInformation($"Warehouse found in DB: {JsonConvert.SerializeObject(warehouse.HopId)}");
            return warehouse;
        }

        public Warehouse Update(Warehouse w)
        {
            throw new NotImplementedException();
        }

    }
}
