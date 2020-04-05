using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IProducer _producer;
        private readonly CustomerContext _customerContext;

        private static List<Client> _clients = new List<Client>()
        {
            new Client()
            {
                ID = 1,
                Name = "José",
                BornDate = new DateTime(2001, 5, 31),
                ClientConditions = EnumClientConditions.Marryied | EnumClientConditions.Children
            },
            new Client() {
                ID = 2,
                Name = "Maria",
                BornDate = new DateTime(1995, 2, 28),
                ClientConditions = EnumClientConditions.Employee | EnumClientConditions.Children | EnumClientConditions.Marryied
            },
            new Client() {
                ID = 3,
                Name = "Romilda",
                BornDate = new DateTime(1945, 6, 8)
            }
        };

        public ClientsController(IProducer producer, CustomerContext customerContext) => (_producer, _customerContext) = (producer, customerContext);

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Client>> Get() => _customerContext.Clients.ToList();//  _clients.ToList();

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Client> Get(int id)
        {
            Client client = _clients.FirstOrDefault(c => c.ID.Equals(id));
            if (client != null)
                return client;
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Client> Post([FromBody] Client client)
        {
            client.ID = _clients.Max(c => c.ID) + 1;
            _clients.Add(client);
            _producer.Broadcast(client, EnumRefreshType.Inserted);
            return client;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Client> Put([FromBody] Client client)
        {
            Client clientFound = _clients.FirstOrDefault(c => c.ID.Equals(client.ID));
            if (clientFound == null)
                return NotFound();
            _clients.Remove(clientFound);
            _clients.Add(client);
            _producer.Broadcast(client, EnumRefreshType.Updated);
            return client;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Client clientFound = _clients.FirstOrDefault(c => c.ID.Equals(id));
            if (clientFound == null)
                return NotFound();
            _clients.Remove(clientFound);
            _producer.Broadcast(clientFound, EnumRefreshType.Deleted);
            return Accepted(value: clientFound);
        }
    }
}
