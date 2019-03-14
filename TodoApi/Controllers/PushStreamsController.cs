using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushStreamsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult GetAction()
        {
            return Ok("Inscrito");
        }
    }
}