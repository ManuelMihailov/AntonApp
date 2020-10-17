using Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntonApp.Models
{
    public class RequestViewModel
    {
        public RequestViewModel(Request request, ICollection<Warehouse> warehouses)
        {
            this.Id = request.Id;
            this.Product = request.Product.Name;
            this.Destination = request.User.City;
            this.AvailableWarehouses = new List<SelectListItem>();
            this.status = request.status;
            foreach (var item in warehouses)
            {
                AvailableWarehouses.Add(new SelectListItem(item.City, item.City));
            }
        }
        public int Id { get; set; }
        public string Product { get; set; }
        public string Destination { get; set; }
        public int status { get; set; }
        public IList<SelectListItem> AvailableWarehouses { get; set; }
    }
}
