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
    public class LogisticsPartnerApiTests
    {
        [Test]
        public void LogisticsPartnerAPI_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var DTOParcel = Builder<DTOs.Parcel>.CreateNew().Build();
            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z0-9]{9}$" });
            string randomTrackingId = randomizerTextRegex.Generate();

            var transferLogicMock = new Mock<ITransferLogic>();

            transferLogicMock
                .Setup(m => m.TransferPackage(It.IsAny<string>(), It.IsAny<BusinessLogic.Entities.Parcel>()))
                .Returns(Builder<BusinessLogic.Entities.Parcel>.CreateNew().With(x => x.TrackingId = randomTrackingId).Build());

            ITransferLogic transferLogic = transferLogicMock.Object;

            var loggerMock = new Mock<ILogger<LogisticsPartnerApiController>>();
            ILogger<LogisticsPartnerApiController> logger = loggerMock.Object;

            LogisticsPartnerApiController logisticsPartnerApi = new LogisticsPartnerApiController(mapper, transferLogic, logger);

            //ACT
            var result = logisticsPartnerApi.TransitionParcel(randomTrackingId, DTOParcel) as ObjectResult;

            //ASSERT
            Assert.AreEqual(200, result?.StatusCode);
        }

        [Test]
        public void LogisticsPartnerAPI_FaultyData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var DTOParcel = Builder<DTOs.Parcel>.CreateNew().Build();
            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z0-9]{9}$" });
            string randomTrackingId = randomizerTextRegex.Generate();

            var transferLogicMock = new Mock<ITransferLogic>();

            transferLogicMock
                .Setup(m => m.TransferPackage(It.IsAny<string>(), It.IsAny<BusinessLogic.Entities.Parcel>()))
                .Returns(Builder<BusinessLogic.Entities.Error>.CreateNew().Build());

            ITransferLogic transferLogic = transferLogicMock.Object;

            var loggerMock = new Mock<ILogger<LogisticsPartnerApiController>>();
            ILogger<LogisticsPartnerApiController> logger = loggerMock.Object;

            LogisticsPartnerApiController logisticsPartnerApi = new LogisticsPartnerApiController(mapper, transferLogic, logger);

            //ACT
            var result = logisticsPartnerApi.TransitionParcel(randomTrackingId, DTOParcel) as ObjectResult;

            //ASSERT
            Assert.AreEqual(400, result?.StatusCode);
        }
    }
}
