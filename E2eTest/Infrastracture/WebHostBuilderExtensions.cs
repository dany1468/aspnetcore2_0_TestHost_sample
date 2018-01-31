using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace E2eTest.Infrastracture
{

    // This file is copied from following url.
    // https://github.com/aspnet/Hosting/blob/dev/src/Microsoft.AspNetCore.TestHost/WebHostBuilderExtensions.cs

    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseSolutionRelativeContentRoot(
            this IWebHostBuilder builder,
            string solutionRelativePath,
            string solutionName = "*.sln")
        {
            return builder.UseSolutionRelativeContentRoot(solutionRelativePath, AppContext.BaseDirectory, solutionName);
        }

        public static IWebHostBuilder UseSolutionRelativeContentRoot(
            this IWebHostBuilder builder,
            string solutionRelativePath,
            string applicationBasePath,
            string solutionName = "*.sln")
        {
            if (solutionRelativePath == null)
            {
                throw new ArgumentNullException(nameof(solutionRelativePath));
            }

            if (applicationBasePath == null)
            {
                throw new ArgumentNullException(nameof(applicationBasePath));
            }

            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionPath = Directory.EnumerateFiles(directoryInfo.FullName, solutionName).FirstOrDefault();
                if (solutionPath != null)
                {
                    builder.UseContentRoot(Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath)));
                    return builder;
                }

                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo.Parent != null);

            throw new InvalidOperationException($"Solution root could not be located using application root {applicationBasePath}.");
        }
    }
}
