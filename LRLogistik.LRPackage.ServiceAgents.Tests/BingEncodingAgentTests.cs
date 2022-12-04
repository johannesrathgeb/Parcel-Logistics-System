using LRLogistik.LRPackage.BusinessLogic;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;

namespace LRLogistik.LRPackage.ServiceAgents.Tests
{
    public class BingEncodingAgentTests
    {

        [Test]
        public void EncodeValidAddress()
        {
            //ARRANGE
            BusinessLogic.Entities.Recipient address = new BusinessLogic.Entities.Recipient()
            {
                Name = "test",
                Street = "Hochstädtplatz 6",
                PostalCode = "1200",
                City = "Wien",
                Country = "Österreich"
            };

            var loggerMock = new Mock<ILogger<BingEncodingAgent>>();
            ILogger<BingEncodingAgent> logger = loggerMock.Object;

            ServiceAgents.BingEncodingAgent bingEncodingAgent = new ServiceAgents.BingEncodingAgent(logger);

            //ACT
            var result = bingEncodingAgent.EncodeAddress(address);

            //ASSERT
            Assert.IsInstanceOf<GeoCoordinate>(result);
        }

        [Test]
        public void EncodeInvalidAddress()
        {
            //ARRANGE
            BusinessLogic.Entities.Recipient address = new BusinessLogic.Entities.Recipient()
            {
                Name = "test",
                Street = "blablabla",
                PostalCode = "bliblablub",
                City = "jaja",
                Country = "neinein"
            };

            var loggerMock = new Mock<ILogger<BingEncodingAgent>>();
            ILogger<BingEncodingAgent> logger = loggerMock.Object;

            ServiceAgents.BingEncodingAgent bingEncodingAgent = new ServiceAgents.BingEncodingAgent(logger);

            //ACT & ASSERT
            Assert.Throws<Exceptions.ServiceAgentsNotFoundException>(() => bingEncodingAgent.EncodeAddress(address));

        }
    }
}