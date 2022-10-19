using AutoMapper;
using FizzWare.NBuilder;
using LRLogistik.LRPackage.BusinessLogic.Interfaces;
using LRLogistik.LRPackage.Services.Controllers;
using LRLogistik.LRPackage.Services.MappingProfiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.Services.Tests
{
    public class SenderApiTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();


            var DTOParcel = Builder<DTOs.Parcel>.CreateNew().Build();
            var BLParcel = Builder<BusinessLogic.Entities.Parcel>.CreateNew().Build();

            var submisssionLogicMock = new Mock<ISubmissionLogic>();

            submisssionLogicMock
                .Setup(m => m.SubmitParcel(It.IsAny<BusinessLogic.Entities.Parcel>()))
                .Returns(BLParcel);

            ISubmissionLogic submissionLogic = submisssionLogicMock.Object;

            SenderApiController senderApi = new SenderApiController(mapper, submissionLogic);

            var result = senderApi.SubmitParcel(DTOParcel) as ObjectResult;

            Assert.AreEqual(201, result?.StatusCode);

        }
    }
}
