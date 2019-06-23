using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRXamarin.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
