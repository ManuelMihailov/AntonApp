using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IProductService
    {
        Task<IList<Item>> GetItemsAsync();
        Task<IList<Request>> GetRequestsAsync();
        Task<IList<Warehouse>> CheckWarehousesAsync(int productId);
        Task ConfirmRequest(int requestId, string warehouseCity);
        Task BuyItem(string name, int userId);
        Task DeliverItem(int id);
        Task CheckFirstRun();
        Task AddProduct(string name, decimal price, int count, string warehouseCity);
    }
}
