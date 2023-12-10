using Swashbuckle.AspNetCore.SwaggerGen;

namespace OLT.Extensions.SwaggerGen.Versioning
{
    public interface IOltOperationFilter : IOperationFilter
    {
        void Apply(SwaggerGenOptions opt);
    }
}
