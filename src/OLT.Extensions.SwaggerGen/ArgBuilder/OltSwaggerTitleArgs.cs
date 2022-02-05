using System.Reflection;

namespace OLT.Extensions.SwaggerGen
{
    public abstract class OltSwaggerTitleArgs<T> : OltSwaggerOperationFilterArgs<T>
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
