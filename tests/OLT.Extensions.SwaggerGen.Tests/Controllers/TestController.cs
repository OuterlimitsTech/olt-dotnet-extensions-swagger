﻿using Microsoft.AspNetCore.Mvc;


namespace OLT.Extensions.SwaggerGen.Tests.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
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

        [HttpGet, Route("ping")]
        public ActionResult<string> Ping(int? value = null)
        {
            return Ok("Pong");
        }

        [HttpGet, Route("fake/{value1:int}/test/{value2}")]
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
        [HttpGet, Route("fake/{value1:int}/test")]
        public ActionResult<string> PostTest(int value1, [FromBody]TestModel model)
        {
            return Ok($"Pong {value1} {model.Id} {model.Value}");
        }


        [HttpGet, Route("values/{routeId:int}")]
        public ActionResult<string> GetAllParties([FromRoute] RouteIdParams routeIdParams)
        {
            return Ok($"RouteId = {routeIdParams.RouteId}");
        }
    }
}