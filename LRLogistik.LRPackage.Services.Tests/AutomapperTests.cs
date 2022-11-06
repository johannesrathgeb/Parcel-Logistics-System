using AutoMapper;
using FizzWare.NBuilder;
using LRLogistik.LRPackage.Services.MappingProfiles;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.Services.Tests
{
    public class AutomapperTests
    {
        /*
        [Test]
        public void DTOParcel_to_EntityParcel()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var DTOParcel = Builder<DTOs.Parcel>.CreateNew().Build();
            var EntityParcel = Builder<BusinessLogic.Entities.Parcel>.CreateNew().Build();

            //ACT
            var EntityParcelConverted = mapper.Map<BusinessLogic.Entities.Parcel>(DTOParcel);


            //ASSERT
            Assert.AreEqual(EntityParcel, EntityParcelConverted);
        }
        */
    }
}
