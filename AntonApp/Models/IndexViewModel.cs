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
            this.Requests = new List<RequestViewModel>();
            this.Items = new List<ItemsViewModel>();
            foreach (var item in items)
            {
                Items.Add(new ItemsViewModel(item));
            }
        }
        public IndexViewModel()
        {

        }

        public List<ItemsViewModel> Items { get; set; }
        public List<RequestViewModel> Requests { get; set; }
        public string RequestId { get; set; }
        public string Warehouse { get; set; }
    }
}
