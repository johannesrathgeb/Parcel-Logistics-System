using FizzWare.NBuilder;
using LRLogistik.LRPackage.BusinessLogic.Entities;

namespace LRLogistik.LRPackage.BusinessLogic.Tests
{
    public class SubmissionLogicTests
    {
        [Test]
        public void SubmitValidParcel()
        {
            //Arrange
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
            var response = new BusinessLogic.SubmissionLogic().SubmitParcel(parcel);
            //Test
            Assert.IsInstanceOf<Parcel>(response);
        }

        [Test]
        public void SubmitInvalidParcel()
        {
            //Arrange
            Parcel parcel = new Parcel()
            {
                Weight = 0.0f,
                Recipient = null,
                Sender = null,
                State = Parcel.StateEnum.InTransportEnum,
                TrackingId = "123"
            };

            //var par = Builder<Parcel>.CreateNew().With(x => x.Weight = 0.0f).Build(); 


            //Act
            var response = new BusinessLogic.SubmissionLogic().SubmitParcel(parcel);
            //Test
            Assert.IsInstanceOf<Error>(response);
        }
    }
}