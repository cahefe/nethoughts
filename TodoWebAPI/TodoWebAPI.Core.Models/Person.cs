using System;
using System.Collections.Generic;
using System.Text;

namespace TodoWebAPI.Core.Models
{
    public class Person
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public long? Document { get; set; }
    }
}
