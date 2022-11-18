using AutoMapper;
using LRLogistik.LRPackage.BusinessLogic.MappingProfiles;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Tests
{
    public class PatricksTests
    {
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }


        [Test]
        public void GeoCoordinateToPoint()
        {
            BusinessLogic.Entities.GeoCoordinate geoCoordinate = new BusinessLogic.Entities.GeoCoordinate() { Lat = 1, Lon = 2 };

            Point point = _mapper.Map<Point>(geoCoordinate);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, point.Coordinate.X);
                Assert.AreEqual(2, point.Coordinate.Y);
            });
        }

        [Test]
        public void PointToGeoCoordinate()
        {
            Point point = new Point(1, 2);

            BusinessLogic.Entities.GeoCoordinate geoCoordinate = _mapper.Map<BusinessLogic.Entities.GeoCoordinate>(point);
            //Point point = _mapper.Map<Point>(geoCoordinate);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, geoCoordinate.Lat);
                Assert.AreEqual(2, geoCoordinate.Lon);
            });
        }


        [Test]
        public void StringToGeometry()
        {
            string str = "{\r\n  \"type\": \"Feature\",\r\n  \"geometry\": {\r\n    \"type\": \"Point\",\r\n    \"coordinates\": [125.6, 10.1]\r\n  },\r\n  \"properties\": {\r\n    \"name\": \"Dinagat Islands\"\r\n  }\r\n}";

            Geometry geometry = _mapper.Map<Geometry>(str);

            //Assert.AreEqual(str, geometry);
            Assert.IsTrue(geometry.IsValid);
        }

        [Test]
        public void GeometryToString()
        {
            string str = "{\r\n  \"type\": \"Feature\",\r\n  \"geometry\": {\r\n    \"type\": \"Point\",\r\n    \"coordinates\": [125.6, 10.1]\r\n  },\r\n  \"properties\": {\r\n    \"name\": \"Dinagat Islands\"\r\n  }\r\n}";

            Geometry geometry = _mapper.Map<Geometry>(str);

            string str2 = _mapper.Map<string>(geometry);

            //Assert.AreEqual(str, geometry);
            Assert.AreEqual(str2, "{\"type\":\"Feature\",\"geometry\":{\"type\":\"Point\",\"coordinates\":[125.6,10.1]}}");
        }

    }
}
