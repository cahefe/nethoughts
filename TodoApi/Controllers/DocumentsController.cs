using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IProducer _producer;

        private static List<Document> _documents = new List<Document>()
        {
            new Document()
            {
                ID = 1,
                Type = EnumDocumentType.RG,
                Number = 12345,
                ExpirationDate = new DateTime(2021, 7, 31)
            },
            new Document() {
                ID = 2,
                Type = EnumDocumentType.CPF,
                Number = 654987,
                ExpirationDate = new DateTime(2039, 11, 17)
            }
        };

        public DocumentsController(IProducer producer) => _producer = producer;

        // GET api/values
        [HttpGet]
        public ActionResult<IList<Document>> Get() => _documents.ToList();

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Document> Get(int id)
        {
            Document document = _documents.FirstOrDefault(d => d.ID.Equals(id));
            if (document != null)
                return document;
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Document> Post([FromBody] Document document)
        {
            document.ID = _documents.Max(d => d.ID) + 1;
            _documents.Add(document);
            _producer.Broadcast(document, EnumRefreshType.Inserted);
            return document;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Document> Put([FromBody] Document document)
        {
            Document documentFound = _documents.FirstOrDefault(c => c.ID.Equals(document.ID));
            if (documentFound == null)
                return NotFound();
            _documents.Remove(documentFound);
            _documents.Add(document);
            _producer.Broadcast(document, EnumRefreshType.Updated);
            return document;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Document documentFound = _documents.FirstOrDefault(c => c.ID.Equals(id));
            if (documentFound == null)
                return NotFound();
            _documents.Remove(documentFound);
            _producer.Broadcast(documentFound, EnumRefreshType.Deleted);
            return Accepted(value: documentFound);
        }
    }
}
