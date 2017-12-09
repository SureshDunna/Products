using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Products.Filters;

namespace Products.IntegrationTests
{
    public abstract class TestBase
    {
        protected readonly TestServer _server;

        protected TestBase()
        {
            _server = CreateServer();
        }

        protected virtual TestServer CreateServer()
        {
            var builder = BuildWebHost(new string[] {})
            .UseEnvironment("IntegrationTests");

            return new TestServer(builder);

        }

        public static IWebHostBuilder BuildWebHost(string[] args)
        {
           return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .ConfigureServices((services) =>
                {
                    services.AddAutofac();

                    services.AddMvc(
                        options =>
                        {
                            options.Filters.Add(typeof(ValidationFilterAttribute));
                            options.Filters.Add(typeof(ExceptionLoggingFilter));
                        }
                    );
                });
        }
    }
}