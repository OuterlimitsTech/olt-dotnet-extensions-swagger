using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace OLT.Extensions.SwaggerGen
{
    public abstract class OltSwaggerOperationFilterArgs<T> : OltSwaggerSecuritySchemeArgs<T>
      where T : OltSwaggerOperationFilterArgs<T>
    {
        internal List<IOltOperationFilter> OperationFilters { get; set; } = new List<IOltOperationFilter>();

        protected OltSwaggerOperationFilterArgs()
        {
        }

        public T WithOperationFilter(OltDefaultValueFilter filter)
        {
            OperationFilters.Add(filter);
            return (T)this;
        }


        public T WithOperationFilter<TScheme>(TScheme scheme) where TScheme : class, IOltOperationFilter, IOperationFilter
        {
            OperationFilters.Add(scheme);
            return (T)this;
        }

    }
}
