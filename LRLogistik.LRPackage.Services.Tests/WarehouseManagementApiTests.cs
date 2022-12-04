using AutoMapper;
using FizzWare.NBuilder;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.Services.Controllers;
using LRLogistik.LRPackage.Services.MappingProfiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.Services.Tests
{
    public class WarehouseManagementApiTests
    {
        [Test]
        public void WarehouseManagementAPI_ExportWarehouse_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var warehouseLogicMock = new Mock<IWarehouseLogic>();

            warehouseLogicMock
                .Setup(m => m.ExportWarehouse())
                .Returns(Builder<BusinessLogic.Entities.Warehouse>.CreateNew().Build());

            IWarehouseLogic warehouseLogic = warehouseLogicMock.Object;

            var loggerMock = new Mock<ILogger<WarehouseManagementApiController>>();
            ILogger<WarehouseManagementApiController> logger = loggerMock.Object;

            WarehouseManagementApiController warehouseManagementApi = new WarehouseManagementApiController(mapper, warehouseLogic, logger);

            //ACT
            var result = warehouseManagementApi.ExportWarehouses() as ObjectResult;

            //ASSERT
            Assert.AreEqual(200, result?.StatusCode);
        }

        [Test]
        public void WarehouseManagementAPI_ExportWarehouse_FaultyData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var warehouseLogicMock = new Mock<IWarehouseLogic>();

            warehouseLogicMock
                .Setup(m => m.ExportWarehouse())
                .Throws<BusinessLogic.Exceptions.BusinessLogicNotFoundException>();

            IWarehouseLogic warehouseLogic = warehouseLogicMock.Object;

            var loggerMock = new Mock<ILogger<WarehouseManagementApiController>>();
            ILogger<WarehouseManagementApiController> logger = loggerMock.Object;

            WarehouseManagementApiController warehouseManagementApi = new WarehouseManagementApiController(mapper, warehouseLogic, logger);

            //ACT
            var result = warehouseManagementApi.ExportWarehouses() as ObjectResult;

            //ASSERT
            Assert.AreEqual(400, result?.StatusCode);
        }

        [Test]
        public void WarehouseManagementAPI_ImportWarehouse_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var warehouseLogicMock = new Mock<IWarehouseLogic>();

            warehouseLogicMock
                .Setup(m => m.ImportWarehouse(It.IsAny<BusinessLogic.Entities.Warehouse>()))
                .Returns("Successfully loaded");

            IWarehouseLogic warehouseLogic = warehouseLogicMock.Object;

            var loggerMock = new Mock<ILogger<WarehouseManagementApiController>>();
            ILogger<WarehouseManagementApiController> logger = loggerMock.Object;

            WarehouseManagementApiController warehouseManagementApi = new WarehouseManagementApiController(mapper, warehouseLogic, logger);

            //ACT
            var result = warehouseManagementApi.ImportWarehouses(Builder<DTOs.Warehouse>.CreateNew().Build()) as ObjectResult;

            //ASSERT
            Assert.AreEqual(200, result?.StatusCode);
        }

        [Test]
        public void WarehouseManagementAPI_ImportWarehouse_Faulty()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var warehouseLogicMock = new Mock<IWarehouseLogic>();

            warehouseLogicMock
                .Setup(m => m.ImportWarehouse(It.IsAny<BusinessLogic.Entities.Warehouse>()))
                .Throws<BusinessLogic.Exceptions.BusinessLogicNotCreatedException>();

            IWarehouseLogic warehouseLogic = warehouseLogicMock.Object;

            var loggerMock = new Mock<ILogger<WarehouseManagementApiController>>();
            ILogger<WarehouseManagementApiController> logger = loggerMock.Object;

            WarehouseManagementApiController warehouseManagementApi = new WarehouseManagementApiController(mapper, warehouseLogic, logger);

            //ACT
            var result = warehouseManagementApi.ImportWarehouses(Builder<DTOs.Warehouse>.CreateNew().Build()) as ObjectResult;

            //ASSERT
            Assert.AreEqual(400, result?.StatusCode);
        }

        [Test]
        public void WarehouseManagementAPI_GetWarehouse_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z]{4}\d{1,4}$" });
            string randomTrackingCode = randomizerTextRegex.Generate();

            var warehouseLogicMock = new Mock<IWarehouseLogic>();

            warehouseLogicMock
                .Setup(m => m.GetWarehouse(It.IsAny<string>()))
                .Returns(Builder<BusinessLogic.Entities.Hop>.CreateNew().Build());

            IWarehouseLogic warehouseLogic = warehouseLogicMock.Object;

            var loggerMock = new Mock<ILogger<WarehouseManagementApiController>>();
            ILogger<WarehouseManagementApiController> logger = loggerMock.Object;

            WarehouseManagementApiController warehouseManagementApi = new WarehouseManagementApiController(mapper, warehouseLogic, logger);

            //ACT
            var result = warehouseManagementApi.GetWarehouse(randomTrackingCode) as ObjectResult;

            //ASSERT
            Assert.AreEqual(200, result?.StatusCode);
        }

        [Test]
        public void WarehouseManagementAPI_GetWarehouse_FaultyData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z]{4}\d{1,4}$" });
            string randomTrackingCode = randomizerTextRegex.Generate();

            var warehouseLogicMock = new Mock<IWarehouseLogic>();

            warehouseLogicMock
                .Setup(m => m.GetWarehouse(It.IsAny<string>()))
                .Throws<BusinessLogic.Exceptions.BusinessLogicNotFoundException>();

            IWarehouseLogic warehouseLogic = warehouseLogicMock.Object;

            var loggerMock = new Mock<ILogger<WarehouseManagementApiController>>();
            ILogger<WarehouseManagementApiController> logger = loggerMock.Object;

            WarehouseManagementApiController warehouseManagementApi = new WarehouseManagementApiController(mapper, warehouseLogic, logger);

            //ACT
            var result = warehouseManagementApi.GetWarehouse(randomTrackingCode) as ObjectResult;

            //ASSERT
            Assert.AreEqual(400, result?.StatusCode);
        }
    }
}
