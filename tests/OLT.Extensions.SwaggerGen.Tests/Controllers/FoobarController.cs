using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;


namespace OLT.Extensions.SwaggerGen.Tests.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/foo-bar")]
    [ApiController]
    public class FoobarController : ControllerBase
    {

        /// <summary>
        /// Log Method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        public ActionResult<string> Log(string value)
        {
            return Ok($"Received {value}");
        }

        [HttpGet, Route("long-method-name")]
        public ActionResult<string> Ping(int? value = null)
        {
            return Ok("Pong");
        }

        [HttpGet, Route("nothing/{value1:int}/test/{value2}")]
        [ApiVersion("2.0", Deprecated = true)]
        public ActionResult<string> Test(int value1, int? value2 = null)
        {
            return Ok($"Pong {value1} {value2}");
        }

        /// <summary>
        /// Creates an Employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/another/1234/test?api-version=1.0
        ///     {        
        ///       "firstName": "Mike",
        ///       "lastName": "Andrew",
        ///       "emailId": "Mike.Andrew@gmail.com"        
        ///     }
        /// </remarks>
        /// <param name="value1"></param>   
        /// <param name="model"></param>   
        [HttpGet, Route("nothing/{value1:int}/test")]
        [ApiVersion("2.0", Deprecated = true)]
        public ActionResult<string> PostTest(int value1, [FromBody] TestModel model)
        {
            return Ok($"Pong {value1} {model.Id} {model.Value}");
        }
    }
}