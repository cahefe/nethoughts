using System;

namespace TodoApi.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public EnumClientConditions ClientConditions { get; set; }
    }
}