using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using E2eTestSample;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace E2eTestSampleTest
{
    public class UnitTest1 : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UnitTest1() {
            //string appRootPath = Path.GetFullPath(Path.Combine(
                            //AppContext.BaseDirectory,
                            //"..", "..", "..", "..", "E2eTestSample"));
            var asm = typeof(Startup).Assembly.GetName().Name;
            //var path = PlatformServices.Default.Application.ApplicationBasePath;
            //var path = PlatformServices.Default.Application.ApplicationBasePath;
            //var contentPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, $@"..\..\..\..\{asm}"));
            string appRootPath = Path.GetFullPath(Path.Combine(
                            AppContext.BaseDirectory,
                            "..", "..", "..", "..", asm));

            // Arrange
            //_server = new TestServer(new WebHostBuilder()
            _server = new TestServer(new WebHostBuilder()
                                     //.UseKestrel()
                                     .UseContentRoot(appRootPath)
                                     .UseStartup<Startup>());
            _client = _server.CreateClient();
            //_client.BaseAddress = new Uri("https://localhost:5001");
        }

        public void Dispose()
        {
        }

        [Fact]
        public async Task ReturnHelloWorld()
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
