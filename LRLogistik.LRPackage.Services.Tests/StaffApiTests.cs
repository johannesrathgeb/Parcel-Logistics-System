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
    public class StaffApiTests
    {
        [Test]
        public void StaffAPI_ReportDelivery_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z0-9]{9}$" });
            string randomTrackingId = randomizerTextRegex.Generate();

            var trackingLogicMock = new Mock<ITrackingLogic>();

            trackingLogicMock
                .Setup(m => m.ReportDelivery(It.IsAny<string>()))
                .Returns("Successfully reported hop");

            ITrackingLogic trackingLogic = trackingLogicMock.Object;

            var loggerMock = new Mock<ILogger<StaffApiController>>();
            ILogger<StaffApiController> logger = loggerMock.Object;

            StaffApiController staffApi = new StaffApiController(mapper, trackingLogic, logger);

            //ACT
            var result = staffApi.ReportParcelDelivery(randomTrackingId) as ObjectResult;

            //ASSERT
            Assert.AreEqual(200, result?.StatusCode);
        }

        [Test]
        public void StaffAPI_ReportDelivery_FaultyData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z0-9]{9}$" });
            string randomTrackingId = randomizerTextRegex.Generate();

            var trackingLogicMock = new Mock<ITrackingLogic>();

            trackingLogicMock
                .Setup(m => m.ReportDelivery(It.IsAny<string>()))
                .Returns(Builder<BusinessLogic.Entities.Error>.CreateNew().Build());

            ITrackingLogic trackingLogic = trackingLogicMock.Object;

            var loggerMock = new Mock<ILogger<StaffApiController>>();
            ILogger<StaffApiController> logger = loggerMock.Object;

            StaffApiController staffApi = new StaffApiController(mapper, trackingLogic, logger);

            //ACT
            var result = staffApi.ReportParcelDelivery(randomTrackingId) as ObjectResult;

            //ASSERT
            Assert.AreEqual(400, result?.StatusCode);
        }

        [Test]
        public void StaffAPI_ReportHop_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z0-9]{9}$" });
            var randomizerTextRegex2 = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z]{4}\d{1,4}$" });
            string randomTrackingId = randomizerTextRegex.Generate();
            string randomTrackingCode = randomizerTextRegex.Generate();

            var trackingLogicMock = new Mock<ITrackingLogic>();

            trackingLogicMock
                .Setup(m => m.ReportHop(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("Successfully reported hop");

            ITrackingLogic trackingLogic = trackingLogicMock.Object;

            var loggerMock = new Mock<ILogger<StaffApiController>>();
            ILogger<StaffApiController> logger = loggerMock.Object;

            StaffApiController staffApi = new StaffApiController(mapper, trackingLogic, logger);

            //ACT
            var result = staffApi.ReportParcelHop(randomTrackingId, randomTrackingCode) as ObjectResult;

            //ASSERT
            Assert.AreEqual(200, result?.StatusCode);
        }

        [Test]
        public void StaffAPI_ReportHop_FaultyData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z0-9]{9}$" });
            var randomizerTextRegex2 = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z]{4}\d{1,4}$" });
            string randomTrackingId = randomizerTextRegex.Generate();
            string randomTrackingCode = randomizerTextRegex.Generate();

            var trackingLogicMock = new Mock<ITrackingLogic>();

            trackingLogicMock
                .Setup(m => m.ReportHop(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Builder<BusinessLogic.Entities.Error>.CreateNew().Build());
            ITrackingLogic trackingLogic = trackingLogicMock.Object;

            var loggerMock = new Mock<ILogger<StaffApiController>>();
            ILogger<StaffApiController> logger = loggerMock.Object;

            StaffApiController staffApi = new StaffApiController(mapper, trackingLogic, logger);

            //ACT
            var result = staffApi.ReportParcelHop(randomTrackingId, randomTrackingCode) as ObjectResult;

            //ASSERT
            Assert.AreEqual(400, result?.StatusCode);
        }
    }

}
