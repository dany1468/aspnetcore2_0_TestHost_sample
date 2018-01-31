using System;
using System.Net.Http;
using System.Threading.Tasks;
using E2eTest.Infrastracture;
using E2eTestSample;
using Xunit;

namespace E2eTest
{
    public class TestUsingInfrastracture
    {
        public HttpClient Client { get; }

        public TestUsingInfrastracture()
        {
            var fixture = new WebApplicationTestFixture<Startup>();
            Client = fixture.Client;
        }

        [Fact]
        public async Task GetHomePage()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/");

            response.EnsureSuccessStatusCode();
            // Assert
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("Application uses",
                responseString);
        }
    }
}
