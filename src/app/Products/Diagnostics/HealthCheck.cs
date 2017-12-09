using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Products.DataAccess;

namespace Products.Diagnostics
{
    public class HealthCheck : HealthCheckBase
    {
        private readonly IMongoDbHealthCheck _mongoDbHealthCheck;
        private readonly ILogger _logger;
        public HealthCheck(ILogger<HealthCheck> logger, IMongoDbHealthCheck mongoDbHealthCheck) : base(logger)
        {
            _mongoDbHealthCheck =  mongoDbHealthCheck;
            _logger = logger;
        }

        protected override void ConfigureHealthChecks()
        {
            Register("MongoDB Health Check", 
            () =>
            {
                try
                {
                    return Task.FromResult(_mongoDbHealthCheck.IsHealthy);
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Error occured while doing the healthcheck {ex}");
                    return Task.FromResult(false);
                }
            });
        }
    }
}