using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TodoApi.Models;
using TodoApi.Results;
using TodoApi.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static List<Client> _clientsRepo = new List<Client>();
        private IPushStream _pushStream;

        public ValuesController(IPushStream pushStream) => _pushStream = pushStream;

        // GET api/values
        [HttpGet]
        public ActionResult<List<Client>> Get() => _clientsRepo;

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Client> Get(int id) => _clientsRepo.FirstOrDefault(c => c.Id.Equals(id));

        // POST api/values
        [HttpPost]
        public ActionResult<Client> Post([FromBody] Client client)
        {
            var clientFound = _clientsRepo.FirstOrDefault(c => c.Id.Equals(client.Id));
            if(clientFound != null)
                _clientsRepo.Remove(clientFound);
            _clientsRepo.Add(client);
            _pushStream.PushInfo(client, ClientFlowEnum.Insert);
            return client;
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
