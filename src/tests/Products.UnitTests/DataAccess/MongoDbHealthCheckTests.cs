using MongoDB.Driver;
using Moq;
using Products.DataAccess;
using Xunit;

namespace Products.UnitTests.DataAccess
{
    public class MongoDbHealthCheckTests
    {
        private readonly Mock<IMongoDbContext> _context;
        private readonly MongoDbHealthCheck _helthCheck;

        public MongoDbHealthCheckTests()
        {
            _context = new Mock<IMongoDbContext>();
            _helthCheck = new MongoDbHealthCheck(_context.Object);
        }

        [Fact]
        public void ishealthy_false_when_client_is_null()
        {
            Assert.False(_helthCheck.IsHealthy);
        }

        [Fact]
        public void ishealthy_false_when_database_is_null()
        {
            _context.Setup(x => x.Client).Returns(new Mock<IMongoClient>().Object);
            Assert.False(_helthCheck.IsHealthy);
        }

        [Fact]
        public void ishealthy_true_when_client_and_database_is_not_null()
        {
            _context.Setup(x => x.Client).Returns(new Mock<IMongoClient>().Object);
            _context.Setup(x => x.Database).Returns(new Mock<IMongoDatabase>().Object);
            Assert.True(_helthCheck.IsHealthy);
        }
    }
}