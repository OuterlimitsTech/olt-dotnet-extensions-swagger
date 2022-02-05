using System.Collections.Generic;

namespace OLT.Extensions.SwaggerGen
{
    public abstract class OltSwaggerSecuritySchemeArgs<T> : OltSwaggerEnabledArgs<T>
        where T : OltSwaggerSecuritySchemeArgs<T>
    {
        internal List<IOltSwaggerSecurityScheme> SecuritySchemes { get; set; } = new List<IOltSwaggerSecurityScheme>();

        protected OltSwaggerSecuritySchemeArgs()
        {
        }

        /// <summary>
        /// Adds JWT bearer token to available authorizations
        /// </summary>
        /// <param name="scheme"><see cref="OltSwaggerJwtBearerToken"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithSecurityScheme(OltSwaggerJwtBearerToken scheme)
        {
            SecuritySchemes.Add(scheme);
            return (T)this;
        }

        /// <summary>
        /// Adds Api Key to available authorizations
        /// </summary>
        /// <param name="scheme"><see cref="OltSwaggerApiKey"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithSecurityScheme(OltSwaggerApiKey scheme)
        {
            SecuritySchemes.Add(scheme);
            return (T)this;
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

    }
}
