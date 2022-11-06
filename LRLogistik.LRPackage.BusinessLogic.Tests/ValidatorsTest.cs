using LRLogistik.LRPackage.BusinessLogic.Entities;
using LRLogistik.LRPackage.BusinessLogic.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Tests
{
    public class ValidatorsTest
    {
        [Test]
        public void RecipientValidatorValidData()
        {
            Recipient recipient = new Recipient()
            {
                Name = "Ioanes",
                City = "Wien",
                Country = "Austria",
                PostalCode = "A-1234",
                Street = "Lederergasse 65"
            }; 

            RecipientValidator recipientValidator = new RecipientValidator();
            var result = recipientValidator.Validate(recipient);

            Assert.IsTrue(result.IsValid);

        }


    }
}
