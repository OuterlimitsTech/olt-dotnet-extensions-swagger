using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OLT.Extensions.SwaggerGen.Versioning
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly OltSwaggerArgs _args;

        public ConfigureSwaggerOptions(
            OltSwaggerArgs args,
            IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
            _args = args;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateApiInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateApiInfo(ApiVersionDescription description)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = _args.Title,
                Version = description.ApiVersion.ToString(),
                Description = description.IsDeprecated ? $"{_args.Description} - {OltSwaggerExtensions.Deprecated}" : _args.Description,
            };

            if (_args.Contact != null)
            {
                openApiInfo.Contact = _args.Contact;
            }

            if (_args.License != null)
            {
                openApiInfo.License = _args.License;
            }

            return openApiInfo;
        }
    }

}




