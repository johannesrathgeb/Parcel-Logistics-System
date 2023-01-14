using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.BusinessLogic.MappingProfiles;
using LRLogistik.LRPackage.Services.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.Services.Tests
{
    public class ParcelWebhookApiTests
    {

        [Test]
        public void ParcelWebhookApi_ListParcelWebhooks_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();


            var parcelWebhookLogicMock = new Mock<IParcelWebhookLogic>();

            parcelWebhookLogicMock
                .Setup(m => m.ListParcelWebhooks(It.IsAny<string>()))
                .Returns(new List<BusinessLogic.Entities.WebhookResponse>() { new BusinessLogic.Entities.WebhookResponse() {Id = 1, TrackingId = "1234", Url="https://moin" } });

            IParcelWebhookLogic parcelWebhookLogic = parcelWebhookLogicMock.Object;

            var loggerMock = new Mock<ILogger<ParcelWebhookApi>>();
            ILogger<ParcelWebhookApi> logger = loggerMock.Object;

            ParcelWebhookApi parcelWebhookApi = new ParcelWebhookApi(mapper, parcelWebhookLogic, logger);

            //ACT
            var result = parcelWebhookApi.ListParcelWebhooks("1234") as ObjectResult; 

            //ASSERT
            Assert.AreEqual(200, result?.StatusCode);
        }

        [Test]
        public void ParcelWebhookApi_ListParcelWebhooks_FaultyData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();


            var parcelWebhookLogicMock = new Mock<IParcelWebhookLogic>();

            parcelWebhookLogicMock
                .Setup(m => m.ListParcelWebhooks(It.IsAny<string>()))
                .Throws<BusinessLogic.Exceptions.BusinessLogicNotFoundException>();

            IParcelWebhookLogic parcelWebhookLogic = parcelWebhookLogicMock.Object;

            var loggerMock = new Mock<ILogger<ParcelWebhookApi>>();
            ILogger<ParcelWebhookApi> logger = loggerMock.Object;

            ParcelWebhookApi parcelWebhookApi = new ParcelWebhookApi(mapper, parcelWebhookLogic, logger);

            //ACT
            var result = parcelWebhookApi.ListParcelWebhooks("1234") as ObjectResult;

            //ASSERT
            Assert.AreEqual(400, result?.StatusCode);
        }

        [Test]
        public void ParcelWebhookApi_SubscribeParcelWebhook_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();


            var parcelWebhookLogicMock = new Mock<IParcelWebhookLogic>();

            parcelWebhookLogicMock
                .Setup(m => m.SubscribeParcelWebhook(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new BusinessLogic.Entities.WebhookResponse() { Id = 1, TrackingId = "1234", Url = "https://moin"});

            IParcelWebhookLogic parcelWebhookLogic = parcelWebhookLogicMock.Object;

            var loggerMock = new Mock<ILogger<ParcelWebhookApi>>();
            ILogger<ParcelWebhookApi> logger = loggerMock.Object;

            ParcelWebhookApi parcelWebhookApi = new ParcelWebhookApi(mapper, parcelWebhookLogic, logger);

            //ACT
            var result = parcelWebhookApi.SubscribeParcelWebhook("1234", "https://moin") as ObjectResult;

            //ASSERT
            Assert.AreEqual(200, result?.StatusCode);
        }

        [Test]
        public void ParcelWebhookApi_SubscribeParcelWebhook_FaultyData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();


            var parcelWebhookLogicMock = new Mock<IParcelWebhookLogic>();

            parcelWebhookLogicMock
                .Setup(m => m.SubscribeParcelWebhook(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<BusinessLogic.Exceptions.BusinessLogicNotFoundException>();

            IParcelWebhookLogic parcelWebhookLogic = parcelWebhookLogicMock.Object;

            var loggerMock = new Mock<ILogger<ParcelWebhookApi>>();
            ILogger<ParcelWebhookApi> logger = loggerMock.Object;

            ParcelWebhookApi parcelWebhookApi = new ParcelWebhookApi(mapper, parcelWebhookLogic, logger);

            //ACT
            var result = parcelWebhookApi.SubscribeParcelWebhook("1234", "https://moin") as ObjectResult;

            //ASSERT
            Assert.AreEqual(400, result?.StatusCode);
        }

        [Test]
        public void ParcelWebhookApi_UnsubscribeParcelWebhook_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var parcelWebhookLogicMock = new Mock<IParcelWebhookLogic>();

            parcelWebhookLogicMock
                .Setup(m => m.UnsubscribeParcelWebhook(It.IsAny<int>())); 

            IParcelWebhookLogic parcelWebhookLogic = parcelWebhookLogicMock.Object;

            var loggerMock = new Mock<ILogger<ParcelWebhookApi>>();
            ILogger<ParcelWebhookApi> logger = loggerMock.Object;

            ParcelWebhookApi parcelWebhookApi = new ParcelWebhookApi(mapper, parcelWebhookLogic, logger);

            //ACT
            var result = parcelWebhookApi.UnsubscribeParcelWebhook(1) as ObjectResult;

            //ASSERT
            Assert.AreEqual(200, result?.StatusCode);
        }

    }
}
