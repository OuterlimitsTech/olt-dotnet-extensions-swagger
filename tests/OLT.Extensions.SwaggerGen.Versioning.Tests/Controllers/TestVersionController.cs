using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace OLT.Extensions.SwaggerGen.Versioning.Tests.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("/api/api-version")]
    public class TestVersionController : ControllerBase
    {

        [ApiVersion("1.0", Deprecated = true)]
        [ApiVersion("2.0")]
        //[ApiVersion("3.0")]
        [HttpGet, Route("one")]
        public ActionResult ApiVersionOne()
        {
            return Ok(new { id = Faker.RandomNumber.Next() });
        }

        [ApiVersion("2.0")]
        //[ApiVersion("3.0")]
        [HttpGet, Route("two")]
        public ActionResult ApiVersionTwo()
        {
            return Ok(new { id = Faker.RandomNumber.Next() });
        }

    }
}
