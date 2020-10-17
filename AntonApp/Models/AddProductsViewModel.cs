using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntonApp.Models
{
    public class AddProductsViewModel
    {
        //string name, decimal price, int count, string warehouseCity
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string WarehouseCity { get; set; }
    }
}
