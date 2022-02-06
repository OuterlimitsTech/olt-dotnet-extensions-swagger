using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace OLT.Extensions.SwaggerGen.Tests
{

    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static string? Title { get; set; }
        public static string? Description { get; set; }
        public static OpenApiContact? Contact { get; set; }
        public static OpenApiLicense? License { get; set; }
        public static OltSwaggerArgs? Args { get; set; }

        protected IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //var args = new OltSwaggerArgs()
            //    .WithTitle(TestStartup.Title)
            //    .WithDescription(TestStartup.Description)
            //    .WithSecurityScheme(new OltSwaggerJwtBearerToken())
            //    .WithSecurityScheme(new OltSwaggerApiKey())
            //    .WithOperationFilter(new OltDefaultValueFilter())
            //    .WithApiContact(TestStartup.Contact)
            //    .WithApiLicense(TestStartup.License)
            //    .WithXmlComments()
            //    .Enable(true);

            if (Args != null)
            {
                services.AddSwaggerWithVersioning(Args);
            }
            
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