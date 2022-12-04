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

        public Entities.Warehouse Create(Warehouse w)
        {
            try
            {
                _logger.LogInformation($"Creating Warehouse in DB");
                _dbContext.Hops.Add(w);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Warehouse successfully created: {JsonConvert.SerializeObject(w.HopId)}");
                return w;
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError($"Warehouse Creation was invalid");
                throw new Entities.Exceptions.DataAccessNotCreatedException("Create", "Warehouse was not created", e);
            }           
        }

        public void Delete(string id)
        {
            Entities.Warehouse warehouse = GetByHopId(id);
            try
            {
                _logger.LogInformation($"Deleting warehouse from DB: {JsonConvert.SerializeObject(warehouse.HopId)}");
                _dbContext.Remove(warehouse);
                _dbContext.SaveChanges();
                _logger.LogInformation($"Warehouse deleted");
            }
            catch(InvalidOperationException e)
            {
                _logger.LogError($"Warehouse Deletion was invalid; Warehouse doesn't exist");
                throw new Entities.Exceptions.DataAccessNotFoundException("Delete", "Warehouse with " + id + " was not deleted", e);
            }
        }

        public Warehouse GetByHopId(string id)
        {
            _logger.LogInformation($"Getting Warehouse from DB: {JsonConvert.SerializeObject(id)}");
            try
            {
                return (Warehouse)_dbContext.Hops.Single(w => w.HopId == id);
            }
            catch(InvalidOperationException e)
            {
                _logger.LogError($"Getting Warehouse was invalid");
                throw new Entities.Exceptions.DataAccessNotFoundException("Delete", "Warehouse with " + id + " not found", e);
            }
        }

        public Warehouse Update(Warehouse w)
        {
            throw new NotImplementedException();
        }

    }
}
