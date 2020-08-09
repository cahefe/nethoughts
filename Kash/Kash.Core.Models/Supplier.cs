using System;
using System.ComponentModel.DataAnnotations;

namespace Kash.Core.Models
{
    public class Supplier
    {
        public short ID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
