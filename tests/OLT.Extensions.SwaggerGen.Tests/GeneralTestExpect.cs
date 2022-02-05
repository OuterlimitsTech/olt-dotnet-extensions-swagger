using Microsoft.OpenApi.Models;

namespace OLT.Extensions.SwaggerGen.Tests
{
    public class GeneralTestExpect
    {
        public GeneralTestExpect(string? title, string? description, bool hasSecurityReq, bool hasPaths)
        {
            Title = title;
            Description = description;
            HasSecurityReq = hasSecurityReq;
            HasPaths = hasPaths;
        }

        public string? Title { get; set; }
        public string? Description { get; set; } 
        public bool HasSecurityReq { get; set; }
        public bool HasPaths { get; set; }
        public OpenApiContact? Contact { get; set; }
        public OpenApiLicense? License { get; set; }
    }
}