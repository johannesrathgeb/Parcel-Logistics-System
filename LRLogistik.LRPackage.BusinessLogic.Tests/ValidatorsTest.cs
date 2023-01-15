﻿using LRLogistik.LRPackage.BusinessLogic.Entities;
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

        [Test]
        public void WarehouseValidatorValidData()
        {
            Warehouse warehouse = new Warehouse()
            {
                Code = "WENB01",
                Description = "Moinsen",
                Level = 1
            };

            WarehouseValidator warehouseValidator = new WarehouseValidator();
            var result = warehouseValidator.Validate(warehouse);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void WarehouseValidatorInvalidData()
        {
            Warehouse warehouse = new Warehouse()
            {
                Code = "WENB01",
                Description = "ß1aasASJK Sassa#++as-22^^°",
                Level = 1
            };

            WarehouseValidator warehouseValidator = new WarehouseValidator();
            var result = warehouseValidator.Validate(warehouse);

            Assert.IsTrue(!result.IsValid);
        }

        [Test]
        public void HopArrivalValidatorValidData()
        {
            HopArrival hopArrival = new HopArrival()
            {
                Code = "WENB01",
                Description = "safdjasdlöfjasdlöfa",
                DateTime = DateTime.Now
            };

            HopArrivalValidator hopArrivalValidator = new HopArrivalValidator(); 

            var res = hopArrivalValidator.Validate(hopArrival);

            Assert.IsTrue(res.IsValid);
        }

        [Test]
        public void  HopArrivalValidatorInvalidData()
        {
            HopArrival hopArrival = new HopArrival()
            {
                Code = "asdföjkljölsadfklösadfjölk2413^^°",
                Description = "sdfa4321234++ää3^^°",
                DateTime = DateTime.Now
            };

            HopArrivalValidator hopArrivalValidator = new HopArrivalValidator();

            var res = hopArrivalValidator.Validate(hopArrival);

            Assert.IsTrue(!res.IsValid);
        }

    }
}
