using System.Reflection;

namespace OLT.Extensions.SwaggerGen
{
    public abstract class OltSwaggerXmlCommentArgs<T> : OltSwaggerOperationFilterArgs<T>
      where T : OltSwaggerXmlCommentArgs<T>
    {
        internal string? XmlFile { get; set; }
        internal bool IncludeXmlComments { get; set; }        
        internal bool XmlFileMissingException { get; set; }

        protected OltSwaggerXmlCommentArgs()
        {
        }

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
