using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Moq;
using Products.DataAccess;
using Products.Extensions;
using Xunit;

namespace Products.UnitTests.Extensions
{
    public class ConfigurationExtensionsTests
    {
        private readonly Mock<IConfiguration> _configuration;

        public ConfigurationExtensionsTests()
        {
            _configuration = new Mock<IConfiguration>();
        }
        
        [Fact]
        public void must_throw_exception_when_configuration_is_null()
        {
            IConfiguration configuration = null;
            Assert.Throws<ArgumentException>(() => configuration.GetConfigValue<MongoDbConfig>("some key"));
        }

        [Fact]
        public void must_throw_exception_when_key_is_null()
        {
            Assert.Throws<ArgumentException>(() => _configuration.Object.GetConfigValue<MongoDbConfig>(null));
        }

        [Fact]
        public void must_throw_exception_when_config_section_is_null()
        {
            Assert.Throws<ArgumentException>(() => _configuration.Object.GetConfigValue<MongoDbConfig>("some key"));
        }

        [Fact]
        public void can_get_config_object_from_config_section()
        {
            var builder = new ConfigurationBuilder();
            var dic = new Dictionary<string, string>
            {
                {"MongoDbConfig:ConnectionString", "localhost"},
                {"MongoDbConfig:Database", "test"}
            };
            builder.AddInMemoryCollection(dic);

            var configuration = builder.Build();
            var configResponse = configuration.GetConfigValue<MongoDbConfig>("MongoDbConfig");

            Assert.NotNull(configResponse);
            Assert.Equal(configResponse.ConnectionString, "localhost");
            Assert.Equal(configResponse.Database, "test");
        }
    }
}