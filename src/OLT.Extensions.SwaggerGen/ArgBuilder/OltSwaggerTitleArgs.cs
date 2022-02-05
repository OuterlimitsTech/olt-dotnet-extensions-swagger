using Microsoft.OpenApi.Models;
using System.Reflection;

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

    public abstract class OltSwaggerTitleArgs<T> : OltSwaggerContactArgs<T>
        where T : OltSwaggerTitleArgs<T>
    {

        internal string Title { get; set; } =             
            Assembly.GetCallingAssembly().GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>()?.Product ??
            "Web Api";


        protected OltSwaggerTitleArgs()
        {
        }

        /// <summary>
        /// Title of API
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="AssemblyProductAttribute"/>
        /// </remarks>
        /// <param name="value"><see cref="string"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithTitle(string value)
        {
            this.Title = value;
            return (T)this;
        }
    }
}
