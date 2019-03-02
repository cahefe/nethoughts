using System.IO;
using System.Threading;

namespace TodoApi.Interfaces
{
    public interface IPushStreamSubscriber
    {
        void OnStreamAvailable(Stream stream, CancellationToken requestAborted);
    }
}