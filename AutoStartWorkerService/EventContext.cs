using AutoStartWorkerService.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoStartWorkerService
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options)
          : base(options)
        { }
        public DbSet<EventDetail> Events { get; set; }
    }
}