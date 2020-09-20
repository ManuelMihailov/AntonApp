using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Item : IItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime StatusChange { get; set; }
    }
}
