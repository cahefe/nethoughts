using System.IO;
using System.Threading;
using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface IConsumer
    {
        void OnStreamAvailable(Stream stream, CancellationToken requestAborted);
    }   
}