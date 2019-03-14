using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface IProducer
    {
        void ProduceInfo(object info, EnumRefreshType refreshType);
    }   
}