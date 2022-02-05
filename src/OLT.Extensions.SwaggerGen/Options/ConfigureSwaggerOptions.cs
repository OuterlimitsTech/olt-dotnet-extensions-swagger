using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OLT.Extensions.SwaggerGen
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly OltSwaggerArgs _options;

        public ConfigureSwaggerOptions(
            OltSwaggerArgs options,
            IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
            _options = options;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            return new OpenApiInfo()
            {
                Title = _options.Title,
                Version = description.ApiVersion.ToString(),
                Description = description.IsDeprecated ? $"{_options.Description} - DEPRECATED" : _options.Description,
            };
        }
    }

}




