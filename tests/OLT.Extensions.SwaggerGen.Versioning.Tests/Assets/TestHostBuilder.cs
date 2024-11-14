using Asp.Versioning;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLT.Extensions.SwaggerGen.Versioning.Tests.Controllers.Models;
using OLT.Extensions.SwaggerGen.Versioning.Tests.Swagger;

namespace OLT.Extensions.SwaggerGen.Versioning.Tests.Assets;

public class SwaggerHostBuilder : Microsoft.AspNetCore.Hosting.WebHostBuilder
{
    public SwaggerHostBuilder()
    {
        
    }
}

public static class TestHostBuilder
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

    public static SwaggerHostBuilder WebHostBuilder<T>(OltSwaggerArgs args) where T : class
    {

        var webBuilder = new SwaggerHostBuilder();

        webBuilder
            .ConfigureAppConfiguration(builder =>
            {
                builder
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddEnvironmentVariables();
            })
            .UseWebRoot(AppContext.BaseDirectory)
            .UseStartup<T>();

        webBuilder.ConfigureServices(services =>
        {
            services.AddSwaggerWithVersioning(args);

            services.AddMvcCore();

            services.AddApiVersioning(opt =>
            {
                opt.ApiVersionReader = ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader(),
                        new QueryStringApiVersionReader("api-version"),
                        new HeaderApiVersionReader("Accept-Version"),
                        new MediaTypeApiVersionReader("v"));

                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                opt.ReportApiVersions = true;
            })
                .AddApiExplorer(opt =>
                {
                    //The format of the version added to the route URL  
                    opt.GroupNameFormat = "'v'VVV";
                    //Tells swagger to replace the version in the controller route  
                    opt.SubstituteApiVersionInUrl = true;
                });

        });

        

        return webBuilder;
    }

}
