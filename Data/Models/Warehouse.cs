using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string City { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
