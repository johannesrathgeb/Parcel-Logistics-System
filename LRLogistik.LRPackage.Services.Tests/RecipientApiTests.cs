using AutoMapper;
using FizzWare.NBuilder;
using LRLogistik.LRPackage.BusinessLogic;
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
    public class RecipientApiTests
    {
        [Test]
        public void RecipientAPI_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            //var DTOParcel = Builder<DTOs.Parcel>.CreateNew().Build();
            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z0-9]{9}$" });
            string randomTrackingId = randomizerTextRegex.Generate();

            var trackingLogicMock = new Mock<ITrackingLogic>();

            trackingLogicMock
                .Setup(m => m.TrackPackage(It.IsAny<string>()))
                .Returns(Builder<BusinessLogic.Entities.Parcel>.CreateNew().With(x => x.TrackingId = randomTrackingId).Build());

            ITrackingLogic trackingLogic = trackingLogicMock.Object;

            var loggerMock = new Mock<ILogger<RecipientApiController>>();
            ILogger<RecipientApiController> logger = loggerMock.Object;

            RecipientApiController recipientApi = new RecipientApiController(mapper, trackingLogic, logger);

            //ACT
            var result = recipientApi.TrackParcel(randomTrackingId) as ObjectResult;

            //ASSERT
            Assert.AreEqual(200, result?.StatusCode);
            //TrackingID überprüfen?
        }

        [Test]
        public void RecipientAPI_FaultyData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            //var DTOParcel = Builder<DTOs.Parcel>.CreateNew().Build();
            var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex { Pattern = @"^[A-Z0-9]{9}$" });
            string randomTrackingId = randomizerTextRegex.Generate();

            var trackingLogicMock = new Mock<ITrackingLogic>();

            trackingLogicMock
                .Setup(m => m.TrackPackage(It.IsAny<string>()))
                .Throws<BusinessLogic.Exceptions.BusinessLogicNotFoundException>();


            ITrackingLogic trackingLogic = trackingLogicMock.Object;

            var loggerMock = new Mock<ILogger<RecipientApiController>>();
            ILogger<RecipientApiController> logger = loggerMock.Object;

            RecipientApiController recipientApi = new RecipientApiController(mapper, trackingLogic, logger);

            //ACT
            var result = recipientApi.TrackParcel(randomTrackingId) as ObjectResult;

            //ASSERT
            Assert.AreEqual(400, result?.StatusCode);
            //TrackingID überprüfen?
        }
    }
}
