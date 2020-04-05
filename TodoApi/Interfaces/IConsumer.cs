using System.IO;
using System.Threading;

namespace TodoApi.Interfaces
{
    public interface IConsumer
    {
        void OnStreamAvailable(Stream stream, CancellationToken requestAborted);
    }   
}