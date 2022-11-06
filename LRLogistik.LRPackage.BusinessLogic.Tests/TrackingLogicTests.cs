using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.MappingProfiles;
using LRLogistik.LRPackage.BusinessLogic.Validators;
using LRLogistik.LRPackage.DataAccess.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Tests
{
    public class TrackingLogicTests
    {


        [Test]
        public void ReportValidDelivery()
        {
            //Arrange
            var parcelRepositoryMock = new Mock<IParcelRepository>();
            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            string trackingId = "PYJRB4HZ6";
            //Act
            var response = new BusinessLogic.TrackingLogic(mapper, parcelRepository).ReportDelivery(trackingId);
            //Assert
            Assert.AreEqual("Successfully reported hop", response);
        }

        [Test]
        public void ReportInvalidDelivery()
        {
            //Arrange
            var parcelRepositoryMock = new Mock<IParcelRepository>();
            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            string trackingId = "123";
            //Act
            var response = new BusinessLogic.TrackingLogic(mapper, parcelRepository).ReportDelivery(trackingId);
            //Assert
            Assert.IsInstanceOf<Error>(response);
        }

        [Test]
        public void ReportValidHop()
        {
            //Arrange
            var parcelRepositoryMock = new Mock<IParcelRepository>();
            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            string trackingId = "PYJRB4HZ6";
            string code = "ABCD1234";
            //Act
            var response = new BusinessLogic.TrackingLogic(mapper, parcelRepository).ReportHop(trackingId, code);
            //Test
            Assert.AreEqual("Successfully reported hop", response);
        }

        [Test]
        public void ReportInvalidHop()
        {
            //Arrange
            var parcelRepositoryMock = new Mock<IParcelRepository>();
            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            string trackingId = "123";
            string code = "ABCD";
            //Act
            var response = new BusinessLogic.TrackingLogic(mapper, parcelRepository).ReportHop(trackingId, code);
            //Test
            Assert.IsInstanceOf<Error>(response);
        }

        //[Test]
        //public void TrackValidPackage()
        //{

        //    var config = new MapperConfiguration(cfg => {
        //        cfg.AddProfile<MappingProfile>();
        //    });
        //    var mapper = config.CreateMapper();
        //    //Arrange
        //    string trackingId = "PYJRB4HZ6";
        //    //Act
        //    var response = new BusinessLogic.TrackingLogic(mapper).TrackPackage(trackingId);
        //    //Test
        //    Assert.IsInstanceOf<Parcel>(response);
        //}

        //[Test]
        //public void TrackInvalidPackage()
        //{

        //    var config = new MapperConfiguration(cfg => {
        //        cfg.AddProfile<MappingProfile>();
        //    });
        //    var mapper = config.CreateMapper();
        //    //Arrange
        //    string trackingId = "123";
        //    //Act
        //    var response = new BusinessLogic.TrackingLogic(mapper).TrackPackage(trackingId);
        //    //Test
        //    Assert.IsInstanceOf<Error>(response);
        //}

        [Test]
        public void TrackValidParcel()
        {
            //ARRANGE
            DataAccess.Entities.Parcel DALParcel = new DataAccess.Entities.Parcel() { TrackingId = "333333333" };

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            string trackingId = "333333333"; 

            var parcelRepositoryMock = new Mock<IParcelRepository>();

            parcelRepositoryMock
                .Setup(m => m.GetByTrackingId(It.IsAny<string>()))
                .Returns(DALParcel);

            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            TrackingLogic trackingLogic= new TrackingLogic(mapper, parcelRepository);

            //ACT & ASSERT

            Assert.IsInstanceOf<Parcel>(trackingLogic.TrackPackage(trackingId));
        }

        [Test]
        public void TrackInvalidParcel()
        {
            //ARRANGE
            DataAccess.Entities.Parcel DALParcel = new DataAccess.Entities.Parcel() { TrackingId = "333333333" };

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            string trackingId = "333";

            var parcelRepositoryMock = new Mock<IParcelRepository>();

            parcelRepositoryMock
                .Setup(m => m.GetByTrackingId(It.IsAny<string>()))
                .Returns(DALParcel);

            IParcelRepository parcelRepository = parcelRepositoryMock.Object;

            TrackingLogic trackingLogic = new TrackingLogic(mapper, parcelRepository);

            //ACT & ASSERT

            Assert.IsInstanceOf<Error>(trackingLogic.TrackPackage(trackingId));
        }

    }
}
