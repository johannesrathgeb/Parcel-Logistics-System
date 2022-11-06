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
        [Test]
        public void SenderAPI_ValidData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var DTOParcel = Builder<DTOs.Parcel>.CreateNew().Build();

            var submisssionLogicMock = new Mock<ISubmissionLogic>();

            submisssionLogicMock
                .Setup(m => m.SubmitParcel(It.IsAny<BusinessLogic.Entities.Parcel>()))
                .Returns(Builder<BusinessLogic.Entities.Parcel>.CreateNew().Build());

            ISubmissionLogic submissionLogic = submisssionLogicMock.Object;

            SenderApiController senderApi = new SenderApiController(mapper, submissionLogic);

            //ACT
            var result = senderApi.SubmitParcel(DTOParcel) as ObjectResult;

            //ASSERT
            Assert.AreEqual(201, result?.StatusCode);

        }

        [Test]
        public void SenderAPI_FaultyData()
        {
            //ARRANGE
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();

            var DTOParcel = Builder<DTOs.Parcel>.CreateNew().Build();
            
            var submisssionLogicMock = new Mock<ISubmissionLogic>();

            submisssionLogicMock
                .Setup(m => m.SubmitParcel(It.IsAny<BusinessLogic.Entities.Parcel>()))
                .Returns(Builder<BusinessLogic.Entities.Error>.CreateNew().Build());

            ISubmissionLogic submissionLogic = submisssionLogicMock.Object;

            SenderApiController senderApi = new SenderApiController(mapper, submissionLogic);

            //ACT
            var result = senderApi.SubmitParcel(DTOParcel) as ObjectResult;

            //ASSERT
            Assert.AreEqual(400, result?.StatusCode);
        }
    }
}
