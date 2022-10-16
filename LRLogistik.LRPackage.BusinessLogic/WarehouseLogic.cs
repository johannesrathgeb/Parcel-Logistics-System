using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.BusinessLogic.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic
{
    public class WarehouseLogic : IWarehouseLogic
    {
        public object ExportWarehouse()
        {
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
                return new Error() { ErrorMessage = "string" };
            }
        }

        public object ImportWarehouse(Warehouse warehouse)
        {
            return "Successfully loaded"; 

            //return new Error() {ErrorMessage = "string"};
        }
    }
}
