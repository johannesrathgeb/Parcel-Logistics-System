using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            _dbContext.Database.EnsureCreated();

            _dbContext.Database.ExecuteSqlRaw("drop table WarehouseNextHops");
            _dbContext.Database.ExecuteSqlRaw("drop table HopArrival");
            _dbContext.Database.ExecuteSqlRaw("drop table Parcels");
            _dbContext.Database.ExecuteSqlRaw("drop table Recipient");
            _dbContext.Database.ExecuteSqlRaw("drop table Hops");

            _dbContext.Database.EnsureCreated();
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

        //maybe unnötig, weils nach code geht
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
                throw new Entities.Exceptions.DataAccessNotFoundException("Delete", "Warehouse with ID " + id + " not found", e);
            }
        }

        public Hop GetByHopCode(string code)
        {
            _dbContext.Database.EnsureCreated();
            _logger.LogInformation($"Getting Warehouse from DB: {JsonConvert.SerializeObject(code)}");
            try
            {
                return _dbContext.Hops.Single(w => w.Code == code);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Getting Warehouse was invalid");
                throw new Entities.Exceptions.DataAccessNotFoundException("Delete", "Warehouse with code " + code + " not found", e);
            }
        }

        public Warehouse Update(Warehouse w)
        {
            throw new NotImplementedException();
        }

        public Warehouse ExportHierachy()
        {
            _dbContext.Database.EnsureCreated();
            _logger.LogInformation($"Getting Warehouse Hierachy from DB");
            try
            {
                var result = _dbContext.Hops.OfType<Warehouse>()
                    .Include(w => w.NextHops)
                    .ThenInclude(nh => nh.Hop)
                    .AsEnumerable()
                    .SingleOrDefault(w => w.Level == 0);

                return result; 
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError($"Getting Warehouse Hierachy was invalid");
                throw new Entities.Exceptions.DataAccessNotFoundException("Delete", "Warehouse Hierachy not found", e);
            }
        }
    }
}
