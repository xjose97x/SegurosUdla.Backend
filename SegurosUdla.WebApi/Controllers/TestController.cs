using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SegurosUdla.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return Ok("Hola, odio PHP");
        }

        [HttpGet("bye")]
        [Authorize]
        public IActionResult Bye()
        {
            return Ok("Hola, \"amo\" PHP");
        }
    }
}

