namespace OLT.Extensions.SwaggerGen
{
    public abstract class OltSwaggerEnabledArgs<T> : OltSwaggerBuilderArgs
       where T : OltSwaggerEnabledArgs<T>
    {
        internal bool IsEnabled { get; set; }

        protected OltSwaggerEnabledArgs()
        {
        }

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
    }

   
}
