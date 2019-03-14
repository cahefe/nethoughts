using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Results
{
    public class PushStreamResult : IActionResult
    {
        private readonly IConsumer _consumer;
        private readonly Action<Stream, CancellationToken> _onStreamAvailable;
        private readonly CancellationToken _requestAborted;
        private readonly MediaTypeHeaderValue _contentType = new MediaTypeHeaderValue("text/event-stream");

        public PushStreamResult(IConsumer consumer)
        {
            _consumer = consumer;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var stream = context.HttpContext.Response.Body;
            context.HttpContext.Response.GetTypedHeaders().ContentType = _contentType;
            _onStreamAvailable(stream, _requestAborted);
            return Task.CompletedTask;
        }
    }
}