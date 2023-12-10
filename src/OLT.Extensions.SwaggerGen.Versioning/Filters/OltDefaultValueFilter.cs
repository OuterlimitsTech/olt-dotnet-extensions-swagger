using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OLT.Extensions.SwaggerGen.Versioning
{

    /// <summary>
    /// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter and documents deprecated controllers and methods
    /// </summary>
    /// <remarks>This <see cref="IOperationFilter"/> is only required due to bugs in the <see cref="SwaggerGenerator"/>.
    /// Once they are fixed and published, this class can be removed.</remarks> 
    public class OltDefaultValueFilter : IOltOperationFilter
    {
        public void Apply(SwaggerGenOptions opt)
        {
            opt.OperationFilter<OltDefaultValueFilter>();
        }

        /// <summary>
        /// Applies the filter to the specified operation using the given context.
        /// </summary>
        /// <param name="operation">The operation to apply the filter to.</param>
        /// <param name="context">The current operation filter context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters != null)
            {
                foreach (var parameter in operation.Parameters)
                {
                    var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                    var routeInfo = description.RouteInfo;
                    if (string.IsNullOrEmpty(parameter.Name))
                    {
                        parameter.Name = description.ModelMetadata?.Name;
                    }

                    parameter.Description ??= description.ModelMetadata?.Description;

                    if (parameter.Name != null && parameter.Name.Equals("api-version") && parameter.In == ParameterLocation.Query)
                    {
                        parameter.AllowEmptyValue = false;
                        if (description.DefaultValue != null)
                        {
                            parameter.Example = new OpenApiString(description.DefaultValue.ToString());
                        }
                    }

                    parameter.Required = parameter.Required || description.IsRequired;

                    if (routeInfo == null)
                    {
                        continue;
                    }

                    parameter.Required |= !routeInfo.IsOptional;

                }
            }

        }

    }
}
