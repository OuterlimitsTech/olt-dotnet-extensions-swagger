using Microsoft.OpenApi.Models;

namespace OLT.Extensions.SwaggerGen
{
    public abstract class OltSwaggerLicenseArgs<T> : OltSwaggerXmlCommentArgs<T>
       where T : OltSwaggerLicenseArgs<T>
    {

        internal OpenApiLicense? License { get; set; }

        protected OltSwaggerLicenseArgs()
        {
        }

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
    }
}
