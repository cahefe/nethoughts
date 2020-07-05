using System;
using System.Threading.Tasks;

namespace ConsoliZookeeper
{
    public interface IZooKeeperClient : IDisposable
    {
        Task Connect();
        Task Disconnect();
        // Task<bool> IsLeader(string service);
    }
}