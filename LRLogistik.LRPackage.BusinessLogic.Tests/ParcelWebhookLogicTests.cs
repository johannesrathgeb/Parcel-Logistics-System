using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.BusinessLogic.MappingProfiles;
using LRLogistik.LRPackage.DataAccess.Entities;
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
    public class ParcelWebhookLogicTests
    {
        [Test]
        public void ListParcelWebhooksValid()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var loggerMock = new Mock<ILogger<ParcelWebhookLogic>>();
            ILogger<ParcelWebhookLogic> logger = loggerMock.Object;

            var parcelRepositoryMock = new Mock<IParcelRepository>();
            IParcelRepository parcelRepository= parcelRepositoryMock.Object;

            var webhookRepositoryMock = new Mock<IWebhookRepository>();

            webhookRepositoryMock
                .Setup(m => m.GetWebhooksForParcel(It.IsAny<string>()))
                .Returns(new List<DataAccess.Entities.WebhookResponse>() { new DataAccess.Entities.WebhookResponse() {Id = 1, TrackingId = "1234", Url = "https://moin"} });

            IWebhookRepository webhookRepository = webhookRepositoryMock.Object;

            IParcelWebhookLogic parcelWebhookLogic = new ParcelWebhookLogic(webhookRepository, mapper, logger, parcelRepository);

            //ACT & ASSERT
            Assert.IsInstanceOf<List<BusinessLogic.Entities.WebhookResponse>>(parcelWebhookLogic.ListParcelWebhooks("1234"));
        }

        [Test]
        public void ListParcelWebhooksNoEntries()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var loggerMock = new Mock<ILogger<ParcelWebhookLogic>>();
            ILogger<ParcelWebhookLogic> logger = loggerMock.Object;

            var parcelRepositoryMock = new Mock<IParcelRepository>();
            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            var webhookRepositoryMock = new Mock<IWebhookRepository>();

            webhookRepositoryMock
                .Setup(m => m.GetWebhooksForParcel(It.IsAny<string>()))
                .Returns(new List<DataAccess.Entities.WebhookResponse>() { });

            IWebhookRepository webhookRepository = webhookRepositoryMock.Object;

            IParcelWebhookLogic parcelWebhookLogic = new ParcelWebhookLogic(webhookRepository, mapper, logger, parcelRepository);

            //ACT & ASSERT
            //Assert.IsInstanceOf<List<BusinessLogic.Entities.WebhookResponse>>(parcelWebhookLogic.ListParcelWebhooks("1234"));

            Assert.AreEqual(0, parcelWebhookLogic.ListParcelWebhooks("1234").Count); 
        }


        [Test]
        public void SubscribeParcelWebhookValid()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var loggerMock = new Mock<ILogger<ParcelWebhookLogic>>();
            ILogger<ParcelWebhookLogic> logger = loggerMock.Object;

            var parcelRepositoryMock = new Mock<IParcelRepository>();

            //parcelRepositoryMock
            //    .Setup(m => m.GetByTrackingId(It.IsAny<string>()))
            //    .Returns(new List<DataAccess.Entities.WebhookResponse>() { });

            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            var webhookRepositoryMock = new Mock<IWebhookRepository>();

            BusinessLogic.Entities.WebhookResponse webhookResponse = new BusinessLogic.Entities.WebhookResponse() {Id = 1, TrackingId = "1234", Url="https://moin" };
            DataAccess.Entities.WebhookResponse webhookResponse2 = new DataAccess.Entities.WebhookResponse() {Id = 1, TrackingId = "1234", Url="https://moin" };

            webhookRepositoryMock
                .Setup(m => m.CreateWebhook(It.IsAny<DataAccess.Entities.WebhookResponse>()))
                .Returns(webhookResponse2);

            IWebhookRepository webhookRepository = webhookRepositoryMock.Object;

            IParcelWebhookLogic parcelWebhookLogic = new ParcelWebhookLogic(webhookRepository, mapper, logger, parcelRepository);


            //ACT & ASSERT
            Assert.AreEqual("1234", parcelWebhookLogic.SubscribeParcelWebhook("1234", "https://moin").TrackingId); 

        }

    }
}
