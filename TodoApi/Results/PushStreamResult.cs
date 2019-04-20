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
    /// ref: https://www.lambda3.com.br/2018/11/criando-uma-api-streaming-com-net-core/
    public class PushStreamResult : IActionResult
    {
        private readonly Action<Stream, CancellationToken> _onStreamAvailable;
        private readonly CancellationToken _requestAborted;
        private readonly MediaTypeHeaderValue _contentType = new MediaTypeHeaderValue("text/event-stream");

        public PushStreamResult(Action<Stream, CancellationToken> onStreamAvailable, CancellationToken requestAborted) => (_onStreamAvailable, _requestAborted) = (onStreamAvailable, requestAborted);

        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.GetTypedHeaders().ContentType = _contentType;
            _onStreamAvailable(context.HttpContext.Response.Body, _requestAborted);
            return Task.CompletedTask;
        }
    }
}