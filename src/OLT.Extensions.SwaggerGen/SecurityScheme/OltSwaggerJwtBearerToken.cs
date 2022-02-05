using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace OLT.Extensions.SwaggerGen
{
    public class OltSwaggerJwtBearerToken : IOltSwaggerSecurityScheme
    {
        public OltSwaggerJwtBearerToken(string scheme = "Bearer")
        {
            Scheme = scheme;
        }

        public string Id => "BearerAuth";
        public string Scheme { get; }
        public string Description => "JWT Authorization header using the Bearer scheme.";

        public void Apply(SwaggerGenOptions opt)
        {
            opt.AddSecurityDefinition(this.Id, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = this.Scheme,
                BearerFormat = "JWT",
                Description = this.Description
            });

            opt.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = this.Id
                            }
                        },
                        Array.Empty<string>()
                    }
                });
        }
    }
}
