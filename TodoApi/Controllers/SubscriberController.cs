using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Results;
using TodoApi.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly IPushStreamSubscriber _pushStreamSubscriber;

        public SubscriberController(IPushStreamSubscriber pushStreamSubscriber) => _pushStreamSubscriber = pushStreamSubscriber;

        [HttpGet]
        [Route("Subscribe")]
        public IActionResult Subcribe() => new PushStreamResult(OnStreamAvailable, "text/event-stream", HttpContext.RequestAborted);

        private void OnStreamAvailable(Stream stream, CancellationToken requestAborted) => _pushStreamSubscriber.OnStreamAvailable(stream, requestAborted);
    }
}