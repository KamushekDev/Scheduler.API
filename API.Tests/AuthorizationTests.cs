using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace API.Tests
{
    public class AuthorizationTests
    {
        private readonly IHostBuilder _hostBuilder;

        public AuthorizationTests()
        {
            _hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.ConfigureAppConfiguration(config =>
                        config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(),
                            "appsettings.Tests.json")));

                    // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseEnvironment("Tests");
                    webHost.UseStartup<Startup>();
                });
        }

        [Fact]
        public async Task Authorization()
        {
            // Arrange
            var host = await _hostBuilder.StartAsync();

            var client = host.GetTestClient();

            var response = await client.GetAsync("/Authorization/login");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Authorization_Unauthorized()
        {
            // Arrange
            var host = await _hostBuilder.StartAsync();

            var client = host.GetTestClient();

            var response = await client.GetAsync("/api/test");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}