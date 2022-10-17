using LRLogistik.LRPackage.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.BusinessLogic.Tests
{
    public class WarehouseLogicTests
    {
        [Test]
        public void ExportWarehouseTest()
        {
            //Arrange

            //Act
            var response = new BusinessLogic.WarehouseLogic().ExportWarehouse();
            //Test
            Assert.IsInstanceOf<Warehouse>(response);
        }

        [Test]
        public void GetValidWarehouse()
        {
            //Arrange
            string code = "ABCD1234";
            //Act
            var response = new BusinessLogic.WarehouseLogic().GetWarehouse(code);
            //Test
            Assert.IsInstanceOf<Hop>(response);
        }

        [Test]
        public void GetInvalidWarehouse()
        {
            //Arrange
            string code = "ABCD";
            //Act
            var response = new BusinessLogic.WarehouseLogic().GetWarehouse(code);
            //Test
            Assert.IsInstanceOf<Error>(response);
        }

        [Test]
        public void ImportWarehouseTest()
        {
            //Arrange
            Warehouse warehouse = new Warehouse();
            //Act
            var response = new BusinessLogic.WarehouseLogic().ImportWarehouse(warehouse);
            //Test
            Assert.AreEqual("Successfully loaded", response);
        }
    }
}
