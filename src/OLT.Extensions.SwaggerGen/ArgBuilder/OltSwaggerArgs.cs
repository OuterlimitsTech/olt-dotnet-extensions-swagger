using Microsoft.OpenApi.Models;
using OLT.Core;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace OLT.Extensions.SwaggerGen
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

        internal OltOptionsApiVersion VersionOptions { get; }
    }

   

}
