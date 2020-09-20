using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntonApp.Models
{
    public class IndexViewModel
    {
        public IndexViewModel( IList<Item> items)
        {
            this.Items = new List<ItemsViewModel>();
            foreach (var item in items)
            {
                Items.Add(new ItemsViewModel(item));
            }
        }
        public List<ItemsViewModel> Items { get; set; }
    }
}
