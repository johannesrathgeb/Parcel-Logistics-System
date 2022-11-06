using AutoMapper;
using FizzWare.NBuilder;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.MappingProfiles;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using Moq;

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
                .Setup(m => m.Create(It.IsAny<DataAccess.Entities.Parcel>()))
                .Returns(DALParcel);

            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            SubmissionLogic submissionLogic = new SubmissionLogic(mapper, parcelRepository);

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
                .Setup(m => m.Create(It.IsAny<DataAccess.Entities.Parcel>()))
                .Returns(DALParcel);

            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            SubmissionLogic submissionLogic = new SubmissionLogic(mapper, parcelRepository);

            //ACT & ASSERT

            Assert.IsInstanceOf<Error>(submissionLogic.SubmitParcel(BLParcel));

        }
    }
}