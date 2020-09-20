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
        Task BuyItem(string name);
        Task DeliverItem(int id);
        Task CheckFirstRun();
    }
}
