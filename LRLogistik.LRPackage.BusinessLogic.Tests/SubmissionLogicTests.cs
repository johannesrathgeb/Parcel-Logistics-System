using AutoMapper;
using FizzWare.NBuilder;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.MappingProfiles;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using LRLogistik.LRPackage.ServiceAgents.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NetTopologySuite.Geometries;

namespace LRLogistik.LRPackage.BusinessLogic.Tests
{
    public class SubmissionLogicTests
    {

        [Test]
        public void SubmitValidParcel()
        {
            //ARRANGE
            DataAccess.Entities.Parcel DALParcel = new DataAccess.Entities.Parcel() { TrackingId = "333333333" };

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var BLParcel = new Parcel()
            {
                TrackingId = "111111111",
                Weight = 1.0f,
                Recipient = new Recipient() { Name = "string", Street = "string", PostalCode = "string", City = "string", Country = "string" },
                Sender = new Recipient() { Name = "string", Street = "string", PostalCode = "string", City = "string", Country = "string" },
                State = Parcel.StateEnum.InTruckDeliveryEnum,
                VisitedHops = new List<HopArrival> { new HopArrival() { Code = "XXXXXX", Description = "string", DateTime = new DateTime() } },
                FutureHops = new List<HopArrival> { new HopArrival() { Code = "XXXXXX", Description = "string", DateTime = new DateTime() } },
            };

            var parcelRepositoryMock = new Mock<IParcelRepository>(); 

            parcelRepositoryMock
                .Setup(m => m.Create(It.IsAny<DataAccess.Entities.Parcel>(), It.IsAny<Point>(), It.IsAny<Point>()))
                .Returns(DALParcel);

            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            var loggerMock = new Mock<ILogger<SubmissionLogic>>();
            ILogger<SubmissionLogic> logger =loggerMock.Object;

            var encodingAgentMock = new Mock<IGeoEncodingAgent>();
            IGeoEncodingAgent encodingAgent = encodingAgentMock.Object;

            SubmissionLogic submissionLogic = new SubmissionLogic(mapper, parcelRepository, logger, encodingAgent);

            //ACT & ASSERT
            
            Assert.NotNull(submissionLogic.SubmitParcel(BLParcel));
            
        }

        [Test]
        public void SubmitInvalidValidParcel()
        {
            //ARRANGE
            DataAccess.Entities.Parcel DALParcel = new DataAccess.Entities.Parcel() { TrackingId = "333333333" };

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var BLParcel = new Parcel()
            {
                TrackingId = "333333333",
                Weight = 0.0f,
                Recipient = new Recipient() { Name = "string", Street = "string", PostalCode = "string", City = "string", Country = "string" },
                Sender = new Recipient() { Name = "string", Street = "string", PostalCode = "string", City = "string", Country = "string" },
                State = Parcel.StateEnum.InTruckDeliveryEnum,
                VisitedHops = new List<HopArrival> { new HopArrival() { Code = "XXXXXX", Description = "string", DateTime = new DateTime() } },
                FutureHops = new List<HopArrival> { new HopArrival() { Code = "XXXXXX", Description = "string", DateTime = new DateTime() } },
            };

            var parcelRepositoryMock = new Mock<IParcelRepository>();

            parcelRepositoryMock
                .Setup(m => m.Create(It.IsAny<DataAccess.Entities.Parcel>(), It.IsAny<Point>(), It.IsAny<Point>()))
                .Returns(DALParcel);

            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            var loggerMock = new Mock<ILogger<SubmissionLogic>>();
            ILogger<SubmissionLogic> logger = loggerMock.Object;

            var encodingAgentMock = new Mock<IGeoEncodingAgent>();
            IGeoEncodingAgent encodingAgent = encodingAgentMock.Object;

            SubmissionLogic submissionLogic = new SubmissionLogic(mapper, parcelRepository, logger, encodingAgent);

            //ACT & ASSERT
            Assert.Throws<BusinessLogic.Exceptions.BusinessLogicNotFoundException>(() => submissionLogic.SubmitParcel(BLParcel));

        }

        [Test]
        public void RoutingSuccess()
        {
            List<DataAccess.Entities.HopArrival> hopArrivals = new List<DataAccess.Entities.HopArrival>();

            DataAccess.Entities.Hop hop1 = new DataAccess.Entities.Hop(); 
            DataAccess.Entities.Hop hop2 = new DataAccess.Entities.Hop(); 


            var parcelRepositoryMock = new Mock<IParcelRepository>();

            parcelRepositoryMock
                .Setup(m => m.Routing(It.IsAny<DataAccess.Entities.Hop>(), It.IsAny<DataAccess.Entities.Hop>()))
                .Returns(hopArrivals);

            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            Assert.IsInstanceOf<List<DataAccess.Entities.HopArrival>>(parcelRepository.Routing(hop1, hop2));                         
        }

    }
}