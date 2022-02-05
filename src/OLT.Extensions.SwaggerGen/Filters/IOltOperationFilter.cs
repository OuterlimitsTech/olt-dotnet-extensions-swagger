using Swashbuckle.AspNetCore.SwaggerGen;

namespace OLT.Extensions.SwaggerGen
{
    public interface IOltOperationFilter : IOperationFilter
    {
        void Apply(SwaggerGenOptions opt);
    }
}
