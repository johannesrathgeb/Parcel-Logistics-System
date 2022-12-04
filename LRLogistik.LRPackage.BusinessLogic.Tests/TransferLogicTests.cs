using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
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
    public class TransferLogicTests
    {

        [Test]
        public void TransferValidParcel()
        {
            //ARRANGE
            DataAccess.Entities.Parcel DALParcel = new DataAccess.Entities.Parcel() { TrackingId = "333333333" };

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();


            string trackingId = "333333333";

            var BLParcel = new Parcel()
            {
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

            var loggerMock = new Mock<ILogger<TransferLogic>>();
            ILogger<TransferLogic> logger = loggerMock.Object;

            TransferLogic transferLogic = new TransferLogic(mapper, parcelRepository, logger);

            //ACT & ASSERT

            Assert.NotNull(transferLogic.TransferPackage(trackingId, BLParcel));

        }

        [Test]
        public void TransferInvalidParcel()
        {
            //ARRANGE
            DataAccess.Entities.Parcel DALParcel = new DataAccess.Entities.Parcel() { TrackingId = "333333333" };

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();


            string trackingId = "333";

            var BLParcel = new Parcel()
            {
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

            var loggerMock = new Mock<ILogger<TransferLogic>>();
            ILogger<TransferLogic> logger = loggerMock.Object;

            TransferLogic transferLogic = new TransferLogic(mapper, parcelRepository, logger);

            //ACT & ASSERT
            Assert.Throws<BusinessLogic.Exceptions.BusinessLogicNotFoundException>(() => transferLogic.TransferPackage(trackingId, BLParcel));
        }


        //[Test]
        //public void TransferValidPackage()
        //{

        //    var config = new MapperConfiguration(cfg => {
        //        cfg.AddProfile<MappingProfile>();
        //    });
        //    var mapper = config.CreateMapper();
        //    //Arrange
        //    string trackingId = "PYJRB4HZ6";
        //    Parcel parcel = new Parcel()
        //    {
        //        Weight = 3.0f,
        //        Recipient = new Recipient()
        //        {
        //            Name = "nameR",
        //            Street = "streetR",
        //            PostalCode = "postalCodeR",
        //            City = "cityR",
        //            Country = "countryR"
        //        },
        //        Sender = new Recipient()
        //        {
        //            Name = "nameS",
        //            Street = "streetS",
        //            PostalCode = "postalCodeS",
        //            City = "cityS",
        //            Country = "countryS"
        //        },
        //        State = Parcel.StateEnum.InTransportEnum,
        //        TrackingId = "123"
        //    }; 
        //    //Act
        //    var response = new BusinessLogic.TransferLogic(mapper).TransferPackage(trackingId, parcel);
        //    //Test
        //    Assert.IsInstanceOf<Parcel>(response);
        //}

        //[Test]
        //public void TransferInvalidPackage()
        //{

        //    var config = new MapperConfiguration(cfg => {
        //        cfg.AddProfile<MappingProfile>();
        //    });
        //    var mapper = config.CreateMapper();
        //    //Arrange
        //    string trackingId = "123";
        //    Parcel parcel = new Parcel()
        //    {
        //        Weight = 0.0f,
        //        Recipient = null,
        //        Sender = null,
        //        State = Parcel.StateEnum.InTransportEnum,
        //        TrackingId = "123"
        //    };
        //    //Act
        //    var response = new BusinessLogic.TransferLogic(mapper).TransferPackage(trackingId, parcel);
        //    //Test
        //    Assert.IsInstanceOf<Error>(response);
        //}
    }
}
