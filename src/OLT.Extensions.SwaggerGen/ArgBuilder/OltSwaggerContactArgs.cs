using Microsoft.OpenApi.Models;

namespace OLT.Extensions.SwaggerGen
{
    public abstract class OltSwaggerContactArgs<T> : OltSwaggerLicenseArgs<T>
        where T : OltSwaggerTitleArgs<T>
    {

        internal OpenApiContact? Contact { get; set; }

        protected OltSwaggerContactArgs()
        {
        }

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
    }
}
