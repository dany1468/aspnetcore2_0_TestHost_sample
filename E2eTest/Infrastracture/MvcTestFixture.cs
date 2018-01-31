using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace E2eTest.Infrastracture
{
    // This file is copied from following url.
    // https://github.com/aspnet/Mvc/blob/dev/test/Microsoft.AspNetCore.Mvc.FunctionalTests/Infrastructure/MvcTestFixture.cs

    public class MvcTestFixture<TStartup> : WebApplicationTestFixture<TStartup>
         where TStartup : class
    {
        public MvcTestFixture()
            : base(Path.Combine("test", "WebSites", typeof(TStartup).Assembly.GetName().Name))
        {
        }

        protected MvcTestFixture(string solutionRelativePath)
            : base(solutionRelativePath)
        {
        }

        protected override TestServer CreateServer(WebHostBuilder builder)
        {
            var originalCulture = CultureInfo.CurrentCulture;
            var originalUICulture = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("en-GB");
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                return base.CreateServer(builder);
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
                CultureInfo.CurrentUICulture = originalUICulture;
            }
        }
    }
}
