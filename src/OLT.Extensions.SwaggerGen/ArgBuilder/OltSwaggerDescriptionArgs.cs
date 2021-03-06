using System.Reflection;

namespace OLT.Extensions.SwaggerGen
{
    public abstract class OltSwaggerDescriptionArgs<T> : OltSwaggerTitleArgs<T>
       where T : OltSwaggerDescriptionArgs<T>
    {
        internal string Description { get; set; } = "Api Methods";

        protected OltSwaggerDescriptionArgs()
        {
        }

        /// <summary>
        /// Description of API
        /// </summary>
        /// <remarks>
        /// Defaults to "Api Methods"
        /// </remarks>
        /// <param name="value"><see cref="string"/></param>
        /// <returns><typeparamref name="T"/></returns>
        public T WithDescription(string value)
        {
            this.Description = value;
            return (T)this;
        }
    }
}
