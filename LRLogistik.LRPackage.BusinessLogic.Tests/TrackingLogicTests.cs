using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Validators;
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
            string trackingId = "PYJRB4HZ6";
            //Act
            var response = new BusinessLogic.TrackingLogic().ReportDelivery(trackingId);
            //Assert
            Assert.AreEqual("Successfully reported hop", response);
        }

        [Test]
        public void ReportInvalidDelivery()
        {
            //Arrange
            string trackingId = "123";
            //Act
            var response = new BusinessLogic.TrackingLogic().ReportDelivery(trackingId);
            //Assert
            Assert.IsInstanceOf<Error>(response);
        }

        [Test]
        public void ReportValidHop()
        {
            //Arrange
            string trackingId = "PYJRB4HZ6";
            string code = "ABCD1234";
            //Act
            var response = new BusinessLogic.TrackingLogic().ReportHop(trackingId, code);
            //Test
            Assert.AreEqual("Successfully reported hop", response);
        }

        [Test]
        public void ReportInvalidHop()
        {
            //Arrange
            string trackingId = "123";
            string code = "ABCD";
            //Act
            var response = new BusinessLogic.TrackingLogic().ReportHop(trackingId, code);
            //Test
            Assert.IsInstanceOf<Error>(response);
        }

        [Test]
        public void TrackValidPackage()
        {
            //Arrange
            string trackingId = "PYJRB4HZ6";
            //Act
            var response = new BusinessLogic.TrackingLogic().TrackPackage(trackingId);
            //Test
            Assert.IsInstanceOf<Parcel>(response);
        }

        [Test]
        public void TrackInvalidPackage()
        {
            //Arrange
            string trackingId = "123";
            //Act
            var response = new BusinessLogic.TrackingLogic().TrackPackage(trackingId);
            //Test
            Assert.IsInstanceOf<Error>(response);
        }
    }
}
