using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contracts
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        ICollection<Item> Items { get; set; }
    }
}
