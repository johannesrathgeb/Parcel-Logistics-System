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

        public object ImportWarehouse(Warehouse warehouse)
        {

            //DataAccess.Entities.Warehouse h = _mapper.Map<DataAccess.Entities.Warehouse>(warehouse); 

            //_warehouseRepository.Create(h);
            _logger.LogInformation($"Importing Warehouse: {JsonConvert.SerializeObject(warehouse)}");
            _warehouseRepository.Create(_mapper.Map<DataAccess.Entities.Warehouse>(warehouse));
            _logger.LogInformation($"Mapped imported Warehouse to DAL: {JsonConvert.SerializeObject(warehouse)}");
            return "Successfully loaded";

            //return new Error() {ErrorMessage = "string"};
        }

        public object ExportWarehouse()
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
                LocationCoordinates = new GeoCoordinate() {Lat = 0.0, Lon = 0.0 },
                NextHops = new List<WarehouseNextHops>() { new WarehouseNextHops() {TraveltimeMins = 0, Hop = new Hop() {HopType = "string", Code = "string", Description = "string", ProcessingDelayMins = 0, LocationName = "string",LocationCoordinates = new GeoCoordinate() {Lat = 0.0, Lon = 0.0 } } } }
            };

            //return new Error() {ErrorMessage = "string"};

        }

        public object GetWarehouse(string code)
        {
            _logger.LogInformation($"Getting Warehouse with code: {JsonConvert.SerializeObject(code)}");
            TrackingCodeValidator trackingCodeValidator = new TrackingCodeValidator();

            var result = trackingCodeValidator.Validate(code);

            if (result.IsValid)
            {
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
            else
            {
                _logger.LogDebug($"Code was invalid!");
                return new Error() { ErrorMessage = "string" };
            }
        }


    }
}
