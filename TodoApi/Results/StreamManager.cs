using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Results
{
    public class StreamManager: IPushStream, IPushStreamSubscriber
    {
        private static int _instanceCounter = 0;
        private static ConcurrentBag<StreamWriter> _clients;

        public StreamManager() 
        {
            _instanceCounter++;
            _clients = new ConcurrentBag<StreamWriter>();
        }
        public void OnStreamAvailable(Stream stream, CancellationToken requestAborted)
        {
            var wait = requestAborted.WaitHandle;
            _clients.Add(new StreamWriter(stream));

            wait.WaitOne();

            StreamWriter ignore;
            _clients.TryTake(out ignore);
        }
        public async void PushInfo(object info, ClientFlowEnum clientFlowEnum)
        {
            foreach (var client in _clients)
            {
                await client.WriteAsync(string.Format("{0}\n", JsonConvert.SerializeObject(new { info, clientFlowEnum, _instanceCounter })));
                await client.FlushAsync();
            }
        }
    }
}