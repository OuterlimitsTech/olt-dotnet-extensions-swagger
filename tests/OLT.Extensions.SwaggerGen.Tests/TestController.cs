using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OLT.Extensions.SwaggerGen.Tests
{
    [Route("api/log")]
    [ApiController]
    public class TestController : ControllerBase
    {


        [AllowAnonymous]
        [HttpPost, Route("")]
        public ActionResult<string> Log(string value)
        {            
            return Ok($"Received {value}");
        }

        [AllowAnonymous]
        [HttpGet, Route("ping")]
        public ActionResult<string> Ping()
        {
            return Ok("Pong");
        }

    }
}