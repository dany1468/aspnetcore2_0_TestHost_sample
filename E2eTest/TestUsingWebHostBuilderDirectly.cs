using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using E2eTestSample;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace E2eTest
{
    public class TestUsingWebHostBuilderDirectly
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TestUsingWebHostBuilderDirectly()
        {
            var asm = typeof(Startup).Assembly.GetName().Name;
            string appRootPath = Path.GetFullPath(Path.Combine(
                            AppContext.BaseDirectory,
                            "..", "..", "..", "..", asm));

            // Arrange
            _server = new TestServer(new WebHostBuilder()
                                     .UseContentRoot(appRootPath)
                                     .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetHomePage()
        {
            // Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("Application uses",
                responseString);
        }
    }
}
