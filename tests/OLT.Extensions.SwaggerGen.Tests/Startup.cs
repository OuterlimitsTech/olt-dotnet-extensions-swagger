using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OLT.Extensions.SwaggerGen.Tests
{

    //This is needed for xUnit DI
    public class Startup
    {

    }

    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public const string Title = "Title 1";
        public const string Description = "Description goes here";

        protected IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var args = new OltSwaggerArgs()
                .WithTitle(Title)
                .WithDescription(Description)
                .WithSecurityScheme(new OltSwaggerJwtBearerToken())
                .WithSecurityScheme(new OltSwaggerApiKey())
                .WithOperationFilter(new OltDefaultValueFilter())
                .Enable(true);

            services.AddSwaggerVersioning(args);
            services.AddMvcCore();

            services.AddApiVersioning(opt =>
                {
                    opt.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                    opt.AssumeDefaultVersionWhenUnspecified = true;
                    opt.DefaultApiVersion = new ApiVersion(1, 0);
                    opt.ReportApiVersions = true;
                })
                .AddVersionedApiExplorer(
                    opt =>
                    {
                        //The format of the version added to the route URL  
                        opt.GroupNameFormat = "'v'VVV";
                        //Tells swagger to replace the version in the controller route  
                        opt.SubstituteApiVersionInUrl = true;
                    });
                    }


        public void Configure(IApplicationBuilder app) //IApplicationBuilder app
        {
            app.UseSwaggerWithVersioning();
        }
    }
}