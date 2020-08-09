using System;
using System.Threading.Tasks;

namespace ConsoliZookeeper
{
    public interface IZKClient : IDisposable
    {
        Task Connect();
        Task Disconnect();
        bool Exists(string nodeName, bool watch = false);
        Task SetAsync<TContent>(string nodeName, TContent value, CreateModeEnum createMode = CreateModeEnum.PERSISTENT);
        Task<TContent> GetAsync<TContent>(string nodeName);
        Task DeleteAsync(string nodeName, bool recursively = false);
    }
}