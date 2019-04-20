using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface IProducer
    {
        void Broadcast(object info, EnumRefreshType refreshType);
    }   
}