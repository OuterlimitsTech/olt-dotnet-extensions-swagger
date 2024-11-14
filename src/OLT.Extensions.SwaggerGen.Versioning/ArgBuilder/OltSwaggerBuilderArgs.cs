using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace OLT.Extensions.SwaggerGen.Versioning
{
    public abstract class OltSwaggerBuilderArgs<T> //: OltSwaggerBuilderArgs
          where T : OltSwaggerBuilderArgs<T>
    {
        internal bool IsEnabled { get; set; }


        /// <summary>
        /// Disables/Enables Swagger
        /// </summary>
        /// <param name="value"><see cref="bool"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T Enable(bool value)
        {
            this.IsEnabled = value;
            return (T)this;
        }


        internal string Description { get; set; } = "Api Methods";


        /// <summary>
        /// Description of API
        /// </summary>
        /// <remarks>
        /// Defaults to "Api Methods"
        /// </remarks>
        /// <param name="value"><see cref="string"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithDescription(string? value)
        {
            this.Description = value ?? this.Description;
            return (T)this;
        }


        internal OpenApiContact? Contact { get; set; }


        /// <summary>
        /// API Contact
        /// </summary>
        /// <param name="value"><see cref="OpenApiContact"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithApiContact(OpenApiContact value)
        {
            this.Contact = value;
            return (T)this;
        }


        internal OpenApiLicense? License { get; set; }

        /// <summary>
        /// API Licensing
        /// </summary>
        /// <param name="value"><see cref="OpenApiLicense"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithApiLicense(OpenApiLicense value)
        {
            this.License = value;
            return (T)this;
        }

        internal List<IOltOperationFilter> OperationFilters { get; set; } = new List<IOltOperationFilter>();


        public T WithOperationFilter(OltDefaultValueFilter filter)
        {
            return this.WithOperationFilter<OltDefaultValueFilter>(filter);
        }


        public T WithOperationFilter<TScheme>(TScheme scheme) where TScheme : class, IOltOperationFilter, IOperationFilter
        {
            OperationFilters.Add(scheme);
            return (T)this;
        }

        internal List<IOltSwaggerSecurityScheme> SecuritySchemes { get; set; } = new List<IOltSwaggerSecurityScheme>();

        /// <summary>
        /// Adds JWT bearer token to available authorizations
        /// </summary>
        /// <param name="scheme"><see cref="OltSwaggerJwtBearerToken"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithSecurityScheme(OltSwaggerJwtBearerToken scheme)
        {
            return this.WithSecurityScheme<OltSwaggerJwtBearerToken>(scheme);
        }

        /// <summary>
        /// Adds Api Key to available authorizations
        /// </summary>
        /// <param name="scheme"><see cref="OltSwaggerApiKey"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithSecurityScheme(OltSwaggerApiKey scheme)
        {
            return this.WithSecurityScheme<OltSwaggerApiKey>(scheme);
        }

        /// <summary>
        /// Adds custom authentication to available authorizations
        /// </summary>
        /// <param name="scheme"><see cref="IOltSwaggerSecurityScheme"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithSecurityScheme<TScheme>(TScheme scheme) where TScheme : class, IOltSwaggerSecurityScheme
        {
            SecuritySchemes.Add(scheme);
            return (T)this;
        }

        internal string Title { get; set; } = "API";

        /// <summary>
        /// Title of API
        /// </summary>
        /// <remarks>
        /// Defaults to "API"
        /// </remarks>
        /// <param name="value"><see cref="string"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithTitle(string? value)
        {
            this.Title = value ?? this.Title;
            return (T)this;
        }

        internal string? XmlFile { get; set; }
        internal bool IncludeXmlComments { get; set; }
        internal bool XmlFileMissingException { get; set; }

        /// <summary>
        /// Includes Xml documenation from API.
        /// </summary>
        /// <remarks>
        /// Project create documenation xml file
        /// </remarks>
        /// <param name="xmlFile"><see cref="string"/> Path and File name of Xml file to include</param>
        /// <param name="missingFileException">Throw Exception if file missing</param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithXmlComments(string xmlFile, bool missingFileException = false)
        {
            this.XmlFile = xmlFile;
            this.IncludeXmlComments = true;
            this.XmlFileMissingException = missingFileException;

            return (T)this;
        }
    }
}
