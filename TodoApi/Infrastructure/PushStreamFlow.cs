using System.Collections.Concurrent;
using System.IO;
using System.Text.Json;
using System.Threading;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Infrastructure
{
    public class PushStreamFlow : IProducer, IConsumer
    {
        private static ConcurrentBag<StreamWriter> _streams;
        static PushStreamFlow() => _streams = new ConcurrentBag<StreamWriter>();
        public void Broadcast(object info, EnumRefreshType refreshType)
        {
            foreach (var stream in _streams)
            {
                string type = info.GetType().FullName;
                string jsonInfo = JsonSerializer.Serialize(new { type, info, refreshType });
                stream.WriteAsync(jsonInfo).Wait();
                stream.FlushAsync().Wait();
            }
        }
        public void OnStreamAvailable(Stream stream, CancellationToken requestAborted)
        {
            var wait = requestAborted.WaitHandle;
            _streams.Add(new StreamWriter(stream));
            wait.WaitOne();

            StreamWriter ignore;
            _streams.TryTake(out ignore);
        }
    }
}