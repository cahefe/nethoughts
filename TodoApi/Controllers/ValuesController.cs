using System;
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

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static List<Client> _clientsRepo;
        private static ConcurrentBag<StreamWriter> _clients;

        static ValuesController()
        {
            _clientsRepo = new List<Client>();
            _clients = new ConcurrentBag<StreamWriter>();
        }

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
            EnviarEvento(client, ClientFlowEnum.Insert);
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
        
        [HttpGet]
        [Route("Streaming")]
        public IActionResult Stream() => new PushStreamResult(OnStreamAvailable, "text/event-stream", HttpContext.RequestAborted);

        private static async Task EnviarEvento(object dados, ClientFlowEnum clientFlow)
        {
            foreach (var client in _clients)
            {
                string jsonEvento = string.Format("{0}\n", JsonConvert.SerializeObject(new { dados, clientFlow }));
                await client.WriteAsync(jsonEvento);
                await client.FlushAsync();
            }
        }
        private void OnStreamAvailable(Stream stream, CancellationToken requestAborted)
        {
            var wait = requestAborted.WaitHandle;
            var client = new StreamWriter(stream);
            _clients.Add(client);

            wait.WaitOne();

            StreamWriter ignore;
            _clients.TryTake(out ignore);
        }
    }
}
