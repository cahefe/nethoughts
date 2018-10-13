using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoWebAPI.Core.Models;
using TodoWebAPI.ServicesInterfaces;

namespace TodoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        IPersonService PersonService;
        public PersonController(IPersonService personService) => PersonService = personService;

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return Ok(PersonService.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Person> Get(long ID)
        {
            return PersonService.Get(ID);
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