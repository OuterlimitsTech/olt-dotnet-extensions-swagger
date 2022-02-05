using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace OLT.Extensions.SwaggerGen.Tests
{
    public static class TestExtensions
    {
        public static IWebHostBuilder WebHostBuilder<T>()
             where T : class
        {
            var webBuilder = new WebHostBuilder();
            webBuilder
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .SetBasePath(AppContext.BaseDirectory)
                        //.AddUserSecrets<T>()
                        //.AddJsonFile("appsettings.json", false, true)
                        .AddEnvironmentVariables();
                })
                .UseStartup<T>();

            return webBuilder;
        }
    }
}