using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Products.DataAccess;
using Products.Diagnostics;
using Xunit;

namespace Products.UnitTests.Diagnostics
{
    public class HealthCheckTests
    {
        private readonly Mock<ILogger<HealthCheck>> _logger;
        private readonly Mock<IMongoDbHealthCheck> _mongoDbHealthCheck;

        public HealthCheckTests()
        {
            _logger = new Mock<ILogger<HealthCheck>>();
            _mongoDbHealthCheck = new Mock<IMongoDbHealthCheck>();
        }

        [Fact]
        public async Task health_check_is_red_when_mongodb_is_not_healthy()
        {
            var healthCheck = new HealthCheck(_logger.Object, _mongoDbHealthCheck.Object);

            await Task.Delay(2000);

            Assert.NotNull(healthCheck.CurrentHealthCheckResult);
            Assert.False(healthCheck.CurrentHealthCheckResult.Healthy);
        }

        [Fact]
        public async Task health_check_is_red_when_mongo_db_health_throws_exception()
        {
            _mongoDbHealthCheck.Setup(x => x.IsHealthy).Throws<Exception>();

            var healthCheck = new HealthCheck(_logger.Object, _mongoDbHealthCheck.Object);

            await Task.Delay(2000);

            Assert.NotNull(healthCheck.CurrentHealthCheckResult);
            Assert.False(healthCheck.CurrentHealthCheckResult.Healthy);

            _logger.VerifyAll();
        }

        [Fact]
        public async Task health_check_is_green_when_mongo_db_health_is_green()
        {
            _mongoDbHealthCheck.Setup(x => x.IsHealthy).Returns(true);

            var healthCheck = new HealthCheck(_logger.Object, _mongoDbHealthCheck.Object);

            await Task.Delay(2000);

            Assert.NotNull(healthCheck.CurrentHealthCheckResult);
            Assert.True(healthCheck.CurrentHealthCheckResult.Healthy);
        }
    }
}