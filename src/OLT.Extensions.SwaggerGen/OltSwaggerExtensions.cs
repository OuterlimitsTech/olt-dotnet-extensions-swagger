using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OLT.Extensions.SwaggerGen
{
    public static class OltSwaggerExtensions
    {
        public static IServiceCollection AddSwaggerVersioning(this IServiceCollection services, OltSwaggerArgs args)
        {
            services.AddSwaggerGen(opt =>
            {
                args.OperationFilters.ToList().ForEach(item => item.Apply(opt));
                args.SecuritySchemes.ToList().ForEach(item => item.Apply(opt));

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);

            });
            services.AddSingleton(args);
            services.ConfigureOptions<ConfigureSwaggerOptions>();

            return services;
        }

        public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
        {
            IServiceProvider services = app.ApplicationServices;
            var provider = services.GetRequiredService<IApiVersionDescriptionProvider>();
            var options = services.GetRequiredService<OltSwaggerArgs>();

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    //options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    var deprecated = description.IsDeprecated ? " [DEPRECATED]" : string.Empty;
                    opt.SwaggerEndpoint($"{description.GroupName}/swagger.json", $"{options.Title} API {description.GroupName}{deprecated}");

                }
            });


            return app;
        }
    }
}


