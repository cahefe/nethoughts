using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Results;
using TodoApi.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushStreamsController : ControllerBase
    {
        private readonly IConsumer _consumer;

        public PushStreamsController(IConsumer consumer) => _consumer = consumer;

        // GET api/values
        [HttpGet]
        public IActionResult GetAction() => new PushStreamResult(_consumer.OnStreamAvailable, HttpContext.RequestAborted);
    }
}