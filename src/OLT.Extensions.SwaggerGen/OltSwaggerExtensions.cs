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
        public const string Deprecated = "DEPRECATED";        

        public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services, OltSwaggerArgs args)
        {
            services.AddSingleton(args);

            if (!args.IsEnabled)
            {
                return services;
            }

            services.AddSwaggerGen(opt =>
            {
                args.OperationFilters.ToList().ForEach(item => item.Apply(opt));
                args.SecuritySchemes.ToList().ForEach(item => item.Apply(opt));

                if (args.IncludeXmlComments)
                {
                    var fileExists = File.Exists(args.XmlFile);
                    if (args.XmlFileMissingException || fileExists)
                    {
                        opt.IncludeXmlComments(args.XmlFile);
                    }                    
                }

            });            
            services.ConfigureOptions<ConfigureSwaggerOptions>();

            return services;
        }

        public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
        {
            IServiceProvider services = app.ApplicationServices;
            var args = services.GetRequiredService<OltSwaggerArgs>();

            if (!args.IsEnabled)
            {
                return app;
            }

            var provider = services.GetRequiredService<IApiVersionDescriptionProvider>();
            

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    var deprecated = description.IsDeprecated ? $" [{Deprecated}]" : string.Empty;
                    opt.SwaggerEndpoint($"{description.GroupName}/swagger.json", $"{args.Title} API {description.GroupName}{deprecated}");

                }
            });


            return app;
        }
    }
}


