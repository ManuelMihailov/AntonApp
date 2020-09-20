using Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntonApp.Models
{
    public class ItemsViewModel
    {
        public ItemsViewModel(IItem item)
        {
            Name = item.Product.Name;
            ChangedOn = item.StatusChange;
            Status = item.StatusId;
            Price = item.Product.Price;
            Id = item.Id;
        }
        public string Name { get; set; }
        public DateTime ChangedOn { get; set; }
        public int Status { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
    }
}
