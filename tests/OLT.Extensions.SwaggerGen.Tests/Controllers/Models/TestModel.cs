using System.ComponentModel;

namespace OLT.Extensions.SwaggerGen.Tests
{
    public class TestModel
    {
        /// <summary>
        /// Id Value
        /// </summary>
        public int Id { get; set; }

        [Description("Value of the item")]
        public int? Value { get; set; }
    }
}