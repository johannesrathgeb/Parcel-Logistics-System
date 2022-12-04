using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.BusinessLogic.Validators;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using LRLogistik.LRPackage.DataAccess.Sql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic
{
    public class WarehouseLogic : IWarehouseLogic
    {

        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        IWarehouseRepository _warehouseRepository;

        public WarehouseLogic(IMapper mapper, IWarehouseRepository repository, ILogger<WarehouseLogic> logger)
        {
            _mapper = mapper;
            _warehouseRepository = repository; 
            _logger = logger;
        }

        public string ImportWarehouse(Warehouse warehouse)
        {
            try
            {
                _logger.LogInformation($"Importing Warehouse: {JsonConvert.SerializeObject(warehouse)}");
                _warehouseRepository.Create(_mapper.Map<DataAccess.Entities.Warehouse>(warehouse));
                _logger.LogInformation($"Mapped imported Warehouse to DAL: {JsonConvert.SerializeObject(warehouse)}");
                return "Successfully loaded";
            }
            catch(DataAccess.Entities.Exceptions.DataAccessNotCreatedException e)
            {
                _logger.LogError($"Warehouse was not imported!");
                throw new BusinessLogic.Exceptions.BusinessLogicNotCreatedException("ImportWarehouse", "Warehouse not imported", e);
            }
        }


        public Hop GetWarehouse(string code)
        {
            try
            {
                TrackingCodeValidator trackingCodeValidator = new TrackingCodeValidator();

                var result = trackingCodeValidator.Validate(code);

                if (!result.IsValid)
                {
                    throw new BusinessLogic.Exceptions.BusinessLogicValidationException("ValidateTrackingCode", "Tracking Code was invalid");
                }

                _logger.LogInformation($"Getting Warehouse with code: {JsonConvert.SerializeObject(code)}");

                return new Hop()
                {
                    HopType = "string",
                    Code = "string",
                    Description = "string",
                    ProcessingDelayMins = 0,
                    LocationName = "string",
                    LocationCoordinates = new GeoCoordinate() { Lat = 0.0, Lon = 0.0 }
                };
            }
            catch(Exceptions.BusinessLogicValidationException e)
            {
                _logger.LogError($"Code was invalid!");
                throw new BusinessLogic.Exceptions.BusinessLogicNotFoundException("GetWarehouse", "Warehouse not found", e);
            }
        }
        public Warehouse ExportWarehouse()
        {
            _logger.LogInformation($"Exporting Warehouse!");
            return new Warehouse()
            {
                Level = 0,
                HopType = "string",
                Code = "string",
                Description = "string",
                ProcessingDelayMins = 0,
                LocationName = "string",
                LocationCoordinates = new GeoCoordinate() { Lat = 0.0, Lon = 0.0 },
                NextHops = new List<WarehouseNextHops>() { new WarehouseNextHops() { TraveltimeMins = 0, Hop = new Hop() { HopType = "string", Code = "string", Description = "string", ProcessingDelayMins = 0, LocationName = "string", LocationCoordinates = new GeoCoordinate() { Lat = 0.0, Lon = 0.0 } } } }
            };
        }
    }
}
