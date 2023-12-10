using Swashbuckle.AspNetCore.SwaggerGen;


namespace OLT.Extensions.SwaggerGen.Versioning
{
    /// <summary>
    /// Security Scheme to define to Swagger Definition
    /// </summary>
    public interface IOltSwaggerSecurityScheme
    {
        /// <summary>
        /// Unique Identifer for Swagger Definition
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Description of the Security Scheme
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Applies the scheme to the Swagger Generation Builder Option
        /// </summary>
        /// <remarks>This is called from the Swagger build process</remarks>
        /// <param name="opt"></param>
        void Apply(SwaggerGenOptions opt);
    }
}
