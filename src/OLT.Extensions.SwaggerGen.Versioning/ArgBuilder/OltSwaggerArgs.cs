namespace OLT.Extensions.SwaggerGen.Versioning
{
    /// <summary>
    /// Swagger Argument Builder
    /// </summary>
    public class OltSwaggerArgs : OltSwaggerBuilderArgs<OltSwaggerArgs>
    {
        public OltSwaggerArgs(OltOptionsApiVersion versionOptions)
        {
            VersionOptions = versionOptions;
        }

        internal OltOptionsApiVersion VersionOptions { get; } = new OltOptionsApiVersion();
    }

   

}
