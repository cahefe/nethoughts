using System;
using System.Collections.Generic;

namespace TodoApi.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual List<Invoice> Invoices { get; set; }

    }
}