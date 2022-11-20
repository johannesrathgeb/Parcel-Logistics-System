using EntityFrameworkCore.Testing.Moq.Helpers;
using LRLogistik.LRPackage.DataAccess.Entities;
using LRLogistik.LRPackage.DataAccess.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace LRLogistik.LRPackage.DataAccess.Tests
{
    public class ParcelRepositoryTests
    {
        protected SampleContext _mockedDbContext;

        [SetUp]
        public void Setup()
        {
            // create in-memory database
            //
            var options = new DbContextOptionsBuilder<SampleContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContextToMock = new SampleContext(options);
            _mockedDbContext = new MockedDbContextBuilder<SampleContext>()
                .UseDbContext(dbContextToMock)
                .UseConstructorWithParameters(options).MockedDbContext;

            // sample data
            var parcels = new[]
            {
                new Parcel()
                {
                    Weight = 2,
                    Recipient = new Recipient()
                    {
                        Name = "Florian Heisl",
                        Street = "Musterstraﬂe 2",
                        PostalCode = "A-1010",
                        City = "Wien",
                        Country = "Austria"
                    },
                    Sender = new Recipient()
                    {
                        Name = "Johannes Lutsch",
                        Street = "Musterstraﬂe 1",
                        PostalCode = "A-1010",
                        City = "Wien",
                        Country = "Austria"
                    },
                    TrackingId = "M5OEG8LWD",
                    State = Parcel.StateEnum.PickupEnum
                },
                new Parcel()
                {
                    Weight = 2,
                    Recipient = new Recipient()
                    {
                        Name = "Florian Heisl",
                        Street = "Musterstraﬂe 2",
                        PostalCode = "A-1010",
                        City = "Wien",
                        Country = "Austria"
                    },
                    Sender = new Recipient()
                    {
                        Name = "Johannes Lutsch",
                        Street = "Musterstraﬂe 1",
                        PostalCode = "A-1010",
                        City = "Wien",
                        Country = "Austria"
                    },
                    TrackingId = "MFVFBW984",
                    State = Parcel.StateEnum.PickupEnum
                },
            };

            //_mockedDbContext.Parcels.AddRange(parcels);
            _mockedDbContext.Set<Parcel>().AddRange(parcels);
            _mockedDbContext.SaveChanges();
        }

        [TearDown]
        public void Cleanup()
        {
            _mockedDbContext.Database.EnsureDeleted();
            _mockedDbContext.Dispose();
        }

        [Test]
        public void GetParcelByValidID()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ParcelRepository>>();
            ILogger<ParcelRepository> logger = loggerMock.Object;
            var repository = new ParcelRepository(_mockedDbContext, logger);

            // Act
            var result = repository.GetByTrackingId("M5OEG8LWD");

            // Assert
            Assert.IsInstanceOf<Parcel>(result);
        }

        [Test]
        public void GetParcelByInvalidID()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ParcelRepository>>();
            ILogger<ParcelRepository> logger = loggerMock.Object;
            var repository = new ParcelRepository(_mockedDbContext, logger);

            // Act
            var result = repository.GetByTrackingId("123");

            // Assert
            Assert.IsInstanceOf<Error>(result);
        }

        [Test]
        public void CreateValidParcel()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ParcelRepository>>();
            ILogger<ParcelRepository> logger = loggerMock.Object;
            var repository = new ParcelRepository(_mockedDbContext, logger);
            Parcel parcel = new Parcel()
            {
                Weight = 2,
                Recipient = new Recipient()
                {
                    Name = "Florian Heisl",
                    Street = "Musterstraﬂe 2",
                    PostalCode = "A-1010",
                    City = "Wien",
                    Country = "Austria"
                },
                Sender = new Recipient()
                {
                    Name = "Johannes Lutsch",
                    Street = "Musterstraﬂe 1",
                    PostalCode = "A-1010",
                    City = "Wien",
                    Country = "Austria"
                },
                TrackingId = "XYXYBW984",
                State = Parcel.StateEnum.PickupEnum
            };
            // Act
            var result = repository.Create(parcel);

            // Assert
            Assert.IsInstanceOf<Parcel>(result);
        }

        [Test]
        public void CreateInvalidParcel()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ParcelRepository>>();
            ILogger<ParcelRepository> logger = loggerMock.Object;
            var repository = new ParcelRepository(_mockedDbContext, logger);

            // Act
            var result = repository.Create(null);

            // Assert
            Assert.IsInstanceOf<Error>(result);
        }

        [Test]
        public void DeleteValid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ParcelRepository>>();
            ILogger<ParcelRepository> logger = loggerMock.Object;
            var repository = new ParcelRepository(_mockedDbContext, logger);

            // Act
            repository.Delete("M5OEG8LWD");
            var result = repository.GetByTrackingId("M5OEG8LWD");
            // Assert
            Assert.IsInstanceOf<Error>(result);
        }

        [Test]
        public void DeleteInvalid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ParcelRepository>>();
            ILogger<ParcelRepository> logger = loggerMock.Object;
            var repository = new ParcelRepository(_mockedDbContext, logger);

            // Act
            repository.Delete("XYXYG8LWY");
            var result = repository.GetByTrackingId("M5OEG8LWD");

            // Assert
            Assert.IsInstanceOf<Parcel>(result);
        }
    }
}