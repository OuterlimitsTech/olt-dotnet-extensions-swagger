using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLT.Extensions.SwaggerGen.Versioning;
using OLT.Extensions.SwaggerGen.Versioning.Tests.Controllers.Models;

namespace OLT.Extensions.SwaggerGen.Versioning.Tests.Swagger;

public class HostStartupNew
{
    //public HostStartupNew(IConfiguration configuration)
    //{
    //    Configuration = configuration;
    //}

    //protected IConfiguration Configuration { get; }

    //public void ConfigureServices(IServiceCollection services)
    //{

        
    //    services.AddSwaggerWithVersioning(new OltSwaggerArgs().WithTitle();

    //    if (swaggerConfig.Title != null)
    //    {
            
    //    }

    //    services.AddMvcCore();

    //    services.AddApiVersioning(opt =>
    //        {
    //            opt.ApiVersionReader = ApiVersionReader.Combine(
    //                    new UrlSegmentApiVersionReader(),
    //                    new QueryStringApiVersionReader("api-version"),
    //                    new HeaderApiVersionReader("Accept-Version"),
    //                    new MediaTypeApiVersionReader("v"));

    //            opt.AssumeDefaultVersionWhenUnspecified = true;
    //            opt.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    //            opt.ReportApiVersions = true;
    //        })
    //        .AddApiExplorer(opt =>
    //        {
    //            //The format of the version added to the route URL  
    //            opt.GroupNameFormat = "'v'VVV";
    //            //Tells swagger to replace the version in the controller route  
    //            opt.SubstituteApiVersionInUrl = true;
    //        });

    //}


    public void Configure(IApplicationBuilder app) //IApplicationBuilder app
    {
        app.UseSwaggerWithVersioning();
    }
}
