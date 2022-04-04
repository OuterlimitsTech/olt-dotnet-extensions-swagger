using System.Reflection;

namespace OLT.Extensions.SwaggerGen
{

    public abstract class OltSwaggerTitleArgs<T> : OltSwaggerContactArgs<T>
        where T : OltSwaggerTitleArgs<T>
    {

        internal string Title { get; set; } = "API";


        protected OltSwaggerTitleArgs()
        {
        }

        /// <summary>
        /// Title of API
        /// </summary>
        /// <remarks>
        /// Defaults to "API"
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
