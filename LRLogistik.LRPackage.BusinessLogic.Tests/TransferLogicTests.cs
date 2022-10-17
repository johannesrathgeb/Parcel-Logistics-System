using LRLogistik.LRPackage.BusinessLogic.Entities;
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
        public void TransferValidPackage()
        {
            //Arrange
            string trackingId = "PYJRB4HZ6";
            Parcel parcel = new Parcel()
            {
                Weight = 3.0f,
                Recipient = new Recipient()
                {
                    Name = "nameR",
                    Street = "streetR",
                    PostalCode = "postalCodeR",
                    City = "cityR",
                    Country = "countryR"
                },
                Sender = new Recipient()
                {
                    Name = "nameS",
                    Street = "streetS",
                    PostalCode = "postalCodeS",
                    City = "cityS",
                    Country = "countryS"
                },
                State = Parcel.StateEnum.InTransportEnum,
                TrackingId = "123"
            }; 
            //Act
            var response = new BusinessLogic.TransferLogic().TransferPackage(trackingId, parcel);
            //Test
            Assert.IsInstanceOf<Parcel>(response);
        }

        [Test]
        public void TransferInvalidPackage()
        {
            //Arrange
            string trackingId = "123";
            Parcel parcel = new Parcel()
            {
                Weight = 0.0f,
                Recipient = null,
                Sender = null,
                State = Parcel.StateEnum.InTransportEnum,
                TrackingId = "123"
            };
            //Act
            var response = new BusinessLogic.TransferLogic().TransferPackage(trackingId, parcel);
            //Test
            Assert.IsInstanceOf<Error>(response);
        }
    }
}
