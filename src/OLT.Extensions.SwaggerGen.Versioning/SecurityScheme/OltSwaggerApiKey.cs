using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace OLT.Extensions.SwaggerGen.Versioning
{
    public class OltSwaggerApiKey : IOltSwaggerSecurityScheme
    {
        public OltSwaggerApiKey(string keyName = "X-API-KEY", ParameterLocation parameterLocation = ParameterLocation.Query)
        {
            KeyName = keyName;
        }

        public string Id => "ApiKey";
        public string KeyName { get; }
        public string Description => $"ApiKey using {this.ParameterLocation}";
        public ParameterLocation ParameterLocation { get; }

        public void Apply(SwaggerGenOptions opt)
        {
            opt.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.ApiKey,
                In = this.ParameterLocation,
                Name = KeyName,
                Description = this.Description,
            });

            opt.AddSecurityRequirement(document => new() { [new OpenApiSecuritySchemeReference(this.KeyName, document)] = [] });

            //opt.AddSecurityRequirement(
            //    new OpenApiSecurityRequirement
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = this.Id
            //                }
            //            },
            //            Array.Empty<string>()
            //        }
            //    });

        }
    }
}
