using System;
using System.ComponentModel.DataAnnotations;

namespace AutoStartWorkerService.Models
{
    public class EventDetail {
        [Key]
        public short ID { get; set; }
        public DateTime Moment { get; set; } = DateTime.Now;
        public decimal Price;
        public string Description { get; set; }
    }
}