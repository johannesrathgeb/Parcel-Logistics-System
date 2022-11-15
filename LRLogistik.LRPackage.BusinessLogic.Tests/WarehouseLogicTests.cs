using AutoMapper;
using FizzWare.NBuilder;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.MappingProfiles;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Tests
{
    public class WarehouseLogicTests
    {
        [Test]
        public void ExportWarehouseTest()
        {
            //Arrange
            var warehouseRepositoryMock = new Mock<IWarehouseRepository>();
            IWarehouseRepository warehouseRepository = warehouseRepositoryMock.Object;

            var loggerMock = new Mock<ILogger<WarehouseLogic>>();
            ILogger<WarehouseLogic> logger = loggerMock.Object;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            //Act
            var response = new BusinessLogic.WarehouseLogic(mapper, warehouseRepository, logger).ExportWarehouse();
            //Test
            Assert.IsInstanceOf<Warehouse>(response);
        }

        [Test]
        public void GetValidWarehouse()
        {
            //Arrange
            var warehouseRepositoryMock = new Mock<IWarehouseRepository>();
            IWarehouseRepository warehouseRepository = warehouseRepositoryMock.Object;

            var loggerMock = new Mock<ILogger<WarehouseLogic>>();
            ILogger<WarehouseLogic> logger = loggerMock.Object;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            string code = "ABCD1234";
            //Act
            var response = new BusinessLogic.WarehouseLogic(mapper, warehouseRepository, logger).GetWarehouse(code);
            //Test
            Assert.IsInstanceOf<Hop>(response);
        }

        [Test]
        public void GetInvalidWarehouse()
        {
            //Arrange
            var warehouseRepositoryMock = new Mock<IWarehouseRepository>();
            IWarehouseRepository warehouseRepository = warehouseRepositoryMock.Object;

            var loggerMock = new Mock<ILogger<WarehouseLogic>>();
            ILogger<WarehouseLogic> logger = loggerMock.Object;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            string code = "ABCD";
            //Act
            var response = new BusinessLogic.WarehouseLogic(mapper, warehouseRepository, logger).GetWarehouse(code);
            //Test
            Assert.IsInstanceOf<Error>(response);
        }

        [Test]
        public void ImportWarehouse()
        {
            //ARRANGE
            DataAccess.Entities.Parcel DALParcel = new DataAccess.Entities.Parcel() { TrackingId = "333333333" };

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            Warehouse BLWarehouse = Builder<Warehouse>.CreateNew().Build();

            DataAccess.Entities.Warehouse DALWarehouse = Builder<DataAccess.Entities.Warehouse>.CreateNew().Build();


            var warehouseRepositoryMock = new Mock<IWarehouseRepository>();

            warehouseRepositoryMock
                .Setup(m => m.Create(It.IsAny<DataAccess.Entities.Warehouse>()))
                .Returns(DALWarehouse);

            IWarehouseRepository warehouseRepository = warehouseRepositoryMock.Object;

            var loggerMock = new Mock<ILogger<WarehouseLogic>>();
            ILogger<WarehouseLogic> logger = loggerMock.Object;

            WarehouseLogic warehouseLogic = new WarehouseLogic(mapper, warehouseRepository, logger);

            //ACT & ASSERT

            Assert.AreEqual("Successfully loaded", warehouseLogic.ImportWarehouse(BLWarehouse));

        }

    }
}
