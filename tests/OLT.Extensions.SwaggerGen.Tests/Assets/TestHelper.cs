using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLT.Extensions.SwaggerGen.Versioning;
using System;

namespace OLT.Extensions.SwaggerGen.Tests
{
    public static class TestHelper
    {
        public static IWebHostBuilder WebHostBuilder<T>() where T : class
        {
            var webBuilder = new WebHostBuilder();
            webBuilder
                .ConfigureAppConfiguration(builder =>
                {
                    builder
                        .SetBasePath(AppContext.BaseDirectory)
                        //.AddUserSecrets<T>()
                        //.AddJsonFile("appsettings.json", true, false)
                        .AddEnvironmentVariables();
                })
                .UseWebRoot(AppContext.BaseDirectory)
                .UseStartup<T>();

            return webBuilder;
        }

        
    }

    public class Startup2
    {
        public Startup2(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }


        public virtual void ConfigureServices(IServiceCollection services)
        {
            //OltServiceCollectionAspnetCoreExtensions.AddOltAspNetCore(services);
            OltServiceCollectionAspnetCoreVersionExtensions.AddApiVersioning(services, new OltOptionsApiVersion());
            services.AddRouting();
        }


        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}