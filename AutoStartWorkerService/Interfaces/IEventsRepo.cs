using AutoStartWorkerService.Models;

namespace AutoStartWorkerService.Interfaces
{
    public interface IEventsRepo
    {
        short Create(EventDetail eventDetail);
        EventDetail Get(short ID);
    }
}