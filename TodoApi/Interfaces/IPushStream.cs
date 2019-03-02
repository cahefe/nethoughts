using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface IPushStream
    {
        void PushInfo(object info, ClientFlowEnum clientFlowEnum);
    }
}