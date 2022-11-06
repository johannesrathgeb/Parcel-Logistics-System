using EntityFrameworkCore.Testing.Moq.Helpers;
using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRLogistik.LRPackage.DataAccess.Tests
{
    public class WarehouseRepositoryTests
    {
        protected SampleContext _mockedDbContext;

        [SetUp]
        public void Setup() { 
            // create in-memory database
            var options = new DbContextOptionsBuilder<SampleContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContextToMock = new SampleContext(options);
            _mockedDbContext = new MockedDbContextBuilder<SampleContext>()
                    .UseDbContext(dbContextToMock)
                    .UseConstructorWithParameters(options).MockedDbContext;

                // sample data
                var warehouses = new[]
                {
                    new Warehouse()
                    {
                        HopId = "12345",
                        HopType = "string",
                        Code = "string",
                        Description = "string",
                        ProcessingDelayMins = 2,
                        LocationName = "string",
                        Level = 1,
                        LocationCoordinates = new NetTopologySuite.Geometries.Point(2, 3),
                        NextHops = new List<WarehouseNextHops>()
                        {
                            new WarehouseNextHops()
                            {
                                WarehouseNextHopsId = "ASDFGHI",
                                TraveltimeMins = 5,
                                Hop = new Hop()
                                {
                                    HopId = "JFAKSL",
                                    HopType = "string",
                                    Code = "string",
                                    Description = "string",
                                    ProcessingDelayMins = 2,
                                    LocationName = "string",
                                    LocationCoordinates = new NetTopologySuite.Geometries.Point(2, 3),
                                }
                            }
                        }
                    },
                    new Warehouse()
                    {
                        HopId = "1234",
                        HopType = "string",
                        Code = "string",
                        Description = "string",
                        ProcessingDelayMins = 2,
                        LocationName = "string",
                        Level = 1,
                        LocationCoordinates = new NetTopologySuite.Geometries.Point(2, 3),
                        NextHops = new List<WarehouseNextHops>()
                        {
                            new WarehouseNextHops()
                            {
                                WarehouseNextHopsId = "ASDFGH",
                                TraveltimeMins = 5,
                                Hop = new Hop()
                                {
                                    HopId = "ASFDWE",
                                    HopType = "string",
                                    Code = "string",
                                    Description = "string",
                                    ProcessingDelayMins = 2,
                                    LocationName = "string",
                                    LocationCoordinates = new NetTopologySuite.Geometries.Point(2, 3),
                                }
                            }
                        }
                    },
                };

            //_mockedDbContext.Parcels.AddRange(parcels);
            _mockedDbContext.Set<Warehouse>().AddRange(warehouses);
            _mockedDbContext.SaveChanges();
            }

        [TearDown]
        public void Cleanup()
        {
            _mockedDbContext.Database.EnsureDeleted();
            _mockedDbContext.Dispose();
        }

        [Test]
        public void GetWarehouseByValidID()
        {
            // Arrange
            var repository = new WarehouseRepository(_mockedDbContext);

            // Act
            var result = repository.GetByHopId("1234");

            // Assert
            Assert.IsInstanceOf<Warehouse>(result);
        }

        [Test]
        public void GetWarehouseByInvalidID()
        {
            // Arrange
            var repository = new WarehouseRepository(_mockedDbContext);

            // Act
            var result = repository.GetByHopId("1");

            // Assert
            Assert.IsInstanceOf<Error>(result);
        }

        [Test]
        public void DeleteValid()
        {
            // Arrange
            var repository = new WarehouseRepository(_mockedDbContext);

            // Act
            repository.Delete("1234");
            var result = repository.GetByHopId("1234");
            // Assert
            Assert.IsInstanceOf<Error>(result);
        }

        [Test]
        public void DeleteInvalid()
        {
            // Arrange
            var repository = new WarehouseRepository(_mockedDbContext);

            // Act
            repository.Delete("1");
            var result = repository.GetByHopId("1234");

            // Assert
            Assert.IsInstanceOf<Warehouse>(result);
        }

        [Test]
        public void CreateValidWarehouse()
        {
            // Arrange
            var repository = new WarehouseRepository(_mockedDbContext);
            Warehouse warehouse = new Warehouse()
            {
                HopId = "123456",
                HopType = "string",
                Code = "string",
                Description = "string",
                ProcessingDelayMins = 2,
                LocationName = "string",
                LocationCoordinates = new NetTopologySuite.Geometries.Point(2, 3),
                Level = 2,
                NextHops = new List<WarehouseNextHops>()
                        {
                            new WarehouseNextHops()
                            {
                                WarehouseNextHopsId = "ASDFG",
                                TraveltimeMins = 5,
                                Hop = new Hop()
                                {
                                    HopId = "HHREDSA",
                                    HopType = "string",
                                    Code = "string",
                                    Description = "string",
                                    ProcessingDelayMins = 2,
                                    LocationName = "string",
                                    LocationCoordinates = new NetTopologySuite.Geometries.Point(2, 3),
                                }
                            }
                        }
            };
            // Act
            var result = repository.Create(warehouse);

            // Assert
            Assert.IsInstanceOf<Warehouse>(result);
        }

        [Test]
        public void CreateInvalidWarehouse()
        {
            // Arrange
            var repository = new WarehouseRepository(_mockedDbContext);

            // Act
            var result = repository.Create(null);

            // Assert
            Assert.IsInstanceOf<Error>(result);
        }

    }
}
