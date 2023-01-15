using EntityFrameworkCore.Testing.Moq.Helpers;
using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Tests
{
    public class WebhookRepositoryTests
    {
        protected SampleContext _mockedDbContext;

        [SetUp]
        public void Setup()
        {
            // create in-memory database
            var options = new DbContextOptionsBuilder<SampleContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContextToMock = new SampleContext(options);
            _mockedDbContext = new MockedDbContextBuilder<SampleContext>()
                .UseDbContext(dbContextToMock)
                .UseConstructorWithParameters(options).MockedDbContext;

            var webhookresponses = new[]
            {
                new WebhookResponse()
                {
                    Id = 1,
                    TrackingId = "123",
                    Url = "http://1"
                },
                new WebhookResponse()
                {
                    Id = 2,
                    TrackingId = "456",
                    Url = "http://2"
                },
                new WebhookResponse()
                {
                    Id = 3,
                    TrackingId = "789",
                    Url = "http://3"
                },
                new WebhookResponse()
                {
                    Id = 4,
                    TrackingId = "789",
                    Url = "http://3"
                },
                new WebhookResponse()
                {
                    Id = 10,
                    TrackingId = "555",
                    Url = "http://3"
                }
            }; 

            _mockedDbContext.Set<WebhookResponse>().AddRange(webhookresponses);
            _mockedDbContext.SaveChanges();
        }

        [TearDown]
        public void Cleanup()
        {
            //TODO DB
            _mockedDbContext.Database.EnsureDeleted();
            _mockedDbContext.Dispose();
        }

        [Test]
        public void GetWebhook_Valid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<WebhookRepository>>();
            ILogger<WebhookRepository> logger = loggerMock.Object;


            var repository = new WebhookRepository(_mockedDbContext, logger);

            // Act
            var result = repository.GetWebhook(1); 

            // Assert
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void GetWebhook_Invalid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<WebhookRepository>>();
            ILogger<WebhookRepository> logger = loggerMock.Object;


            var repository = new WebhookRepository(_mockedDbContext, logger);
 
            //Act & Assert
            Assert.Throws<Entities.Exceptions.DataAccessNotFoundException>(() => repository.GetWebhook(5));
        }

        [Test]
        public void CreateWebhook_Valid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<WebhookRepository>>();
            ILogger<WebhookRepository> logger = loggerMock.Object;

            var repository = new WebhookRepository(_mockedDbContext, logger);

            WebhookResponse webhookResponse1 = new WebhookResponse()
            {
                Id = 5,TrackingId = "123",Url = "http://1"
            };

            // Act
            var result = repository.CreateWebhook(webhookResponse1); 

            // Assert
            Assert.IsInstanceOf<WebhookResponse>(result);
        }

        [Test]
        public void DeleteWebhook_Invalid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<WebhookRepository>>();
            ILogger<WebhookRepository> logger = loggerMock.Object;

            var repository = new WebhookRepository(_mockedDbContext, logger);

            //Act & Assert
            Assert.Throws<Entities.Exceptions.DataAccessNotFoundException>(() => repository.DeleteWebhook(5));
        }


        [Test]
        public void GetWebhooksForParcel_Valid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<WebhookRepository>>();
            ILogger<WebhookRepository> logger = loggerMock.Object;

            var repository = new WebhookRepository(_mockedDbContext, logger);

            // Act
            var result = repository.GetWebhooksForParcel("789");

            // Assert
            Assert.AreEqual(2, result.Count()); 
        }

        [Test]
        public void GetWebhooksForParcel_Invalid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<WebhookRepository>>();
            ILogger<WebhookRepository> logger = loggerMock.Object;

            var repository = new WebhookRepository(_mockedDbContext, logger);

            //Act & Assert
            Assert.AreEqual(0, repository.GetWebhooksForParcel("9").Count);
        }

        //[Test]
        //public void UpdateWebhook_Valid()
        //{
        //    // Arrange
        //    var loggerMock = new Mock<ILogger<WebhookRepository>>();
        //    ILogger<WebhookRepository> logger = loggerMock.Object;

        //    var repository = new WebhookRepository(_mockedDbContext, logger);

        //    WebhookResponse webhookResponse1 = new WebhookResponse()
        //    {
        //        Id = 10,
        //        TrackingId = "123",
        //        Url = "http://1"
        //    };

        //    // Act
        //    var result = repository.UpdateWebhook(webhookResponse1);

        //    // Assert
        //    Assert.AreEqual("123", result.TrackingId);
        //}

        //[Test]
        //public void UpdateWebhook_Invalid()
        //{
        //    // Arrange
        //    var loggerMock = new Mock<ILogger<WebhookRepository>>();
        //    ILogger<WebhookRepository> logger = loggerMock.Object;

        //    var repository = new WebhookRepository(_mockedDbContext, logger);

        //    WebhookResponse webhookResponse1 = new WebhookResponse()
        //    {
        //        Id = 7,
        //        TrackingId = "123",
        //        Url = "http://1"
        //    };

        //    //ACT & ASSERT
        //    Assert.Throws<Entities.Exceptions.DataAccessNotFoundException>(() => repository.UpdateWebhook(webhookResponse1));
        //}
    }
}
