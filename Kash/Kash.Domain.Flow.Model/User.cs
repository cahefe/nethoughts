using System;
using System.ComponentModel.DataAnnotations;

namespace Kash.Domain.Flow.Model
{
    public class User : EntryInfo
    {
        [Key]
        public long ID { get; set; }
        [MaxLength(32)]
        public string login { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }
    }
}
