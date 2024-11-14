using Microsoft.OpenApi.Models;

namespace OLT.Extensions.SwaggerGen.Versioning.Tests.Controllers.Models
{
    public class GeneralTestParams
    {
        private readonly string Deprecated = $" - {OltSwaggerExtensions.Deprecated}";

        public GeneralTestParams(string? title, string? description, bool hasSecurityReq, bool hasPaths)
        {
            Title = title ?? Guid.NewGuid().ToString();
            Description = description;
            HasSecurityReq = hasSecurityReq;
            HasPaths = hasPaths;
        }

        public string Title { get; set; }
        public string? Description { get; set; }        
        public bool HasSecurityReq { get; set; }
        public bool HasPaths { get; set; }
        public OpenApiContact? Contact { get; set; }
        public OpenApiLicense? License { get; set; }

        public string GetExpectedDescription(bool completelyDeprecated)
        {
            return (completelyDeprecated ? BuildDeprecatedDescription() : Description) ?? Guid.NewGuid().ToString();
        }

        public string? BuildDeprecatedDescription()
        {
            return $"{Description}{Deprecated}";
        }

    }
}