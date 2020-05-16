using System;
using System.Linq;
using AutoStartWorkerService.Interfaces;
using AutoStartWorkerService.Models;
using Microsoft.Extensions.Logging;

namespace AutoStartWorkerService.Repo
{
    public class EventsRepo : IEventsRepo
    {
        readonly EventContext _eventContext;
        readonly ILogger _logger;
        public EventsRepo(EventContext eventContext, ILogger<EventsRepo> logger)
        {
            _eventContext = eventContext ?? throw new ArgumentNullException(nameof(eventContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public short Create(EventDetail eventDetail)
        {
            _eventContext.Add(eventDetail);
            _eventContext.SaveChanges();
            _logger.LogInformation($"Evento {eventDetail.ID} foi salvo com sucesso");
            return eventDetail.ID;
        }

        public EventDetail Get(short ID) => _eventContext.Events.FirstOrDefault(e => e.ID == ID);
    }
}