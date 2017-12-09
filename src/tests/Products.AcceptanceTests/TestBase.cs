using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Products.AcceptanceTests
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
            var builder = Program.BuildWebHost(new string[] {})
            .UseEnvironment("AcceptanceTests");

            return new TestServer(builder);

        }
    }
}