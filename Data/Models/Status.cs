using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Status : IStatus
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
