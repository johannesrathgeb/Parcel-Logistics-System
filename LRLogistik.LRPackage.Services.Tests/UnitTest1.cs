using NUnit.Framework;
using System.Reflection.Emit;

namespace LRLogistik.LRPackage.Services.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ////Arrange
            //string invalidTrackingId = "!!!";
            //string invalidCode = "!!!";
            //var randomParcel = Builder<DTOs.Parcel>.CreateNew.Build();

            //var staffLogicMock = new Mock<IStaffLogic>();
            //staffLogicMock.Setup(x => x.ReportParcelHop(invalidTrackingId, invalidCode));

            //IStaffLogic staffLogic = staffLogicMock.Object;
            //var mapperConfig = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile(newMappingProfile());
            //});
            //StaffApiController staffApi = new StaffApiController(staffLogic, new ListMapper(mapperConfig));

            ////Act
            //var response = staffApi.ReportParcelHop(invalidTrackingId, invalidCode);
            //var result = response = response as ObjectResult;

            ////Test
            //Assert.AreEqual(result?.StatusCode, 400);
        }
    }
}