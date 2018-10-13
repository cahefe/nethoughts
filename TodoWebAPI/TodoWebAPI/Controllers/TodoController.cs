using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoWebAPI.Core.Models;
using TodoWebAPI.ServicesInterfaces;

namespace TodoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        ITodoService TodoService;
        public TodoController(ITodoService todoService) => TodoService = todoService;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return Ok(TodoService.GetAll());
            //return BadRequest(TodoService.GetAll());
            //return NotFound(TodoService.GetAll());
            //return Created("/apt/result", TodoService.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(long ID)
        {
            return TodoService.Get(ID);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}