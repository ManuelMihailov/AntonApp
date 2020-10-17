using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly AntonDatabaseContext dbContext;
        public ProductService(AntonDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<Item>> GetItemsAsync()
        {
            var items = await dbContext.Items.Include(p => p.Product).Include(p => p.Warehouse).ToListAsync();
            return items;
        }

        public async Task<IList<Request>> GetRequestsAsync()
        {
            var requests = await dbContext.Requests.Include(p => p.Product).Include(p => p.User).ToListAsync();
            return requests;
        }

        public async Task<IList<Warehouse>> CheckWarehousesAsync(int productId)
        {
            var warehouses = await dbContext.Warehouses.Where(p => p.Items.Where(z => z.ProductId == productId).Any()).ToListAsync();
            return warehouses;
        }

        public async Task BuyItem(string name, int userId)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
            var request = new Request()
            {
                UserId = userId,
                ProductId = product.Id,
                status = 0
            };

            await dbContext.Requests.AddAsync(request);
            await dbContext.SaveChangesAsync();
        }

        public async Task ConfirmRequest(int requestId, string warehouseCity)
        {
            var request = await dbContext.Requests.FirstAsync(p => p.Id == requestId);
            var itemToDeliver = await dbContext.Items.Include(p => p.Warehouse).Where(p => p.StatusId == 1 && p.Warehouse.City == warehouseCity && p.ProductId == request.ProductId).FirstAsync();
            request.status = 1;
            request.ItemID = itemToDeliver.Id;
            itemToDeliver.StatusId = 3;
            itemToDeliver.StatusChange = DateTime.Now;
            await dbContext.SaveChangesAsync();

        }

        public async Task DeliverItem(int id)
        {
            var item = await dbContext.Items.Where(p => p.Id == id && p.StatusId == 2).FirstOrDefaultAsync();
            item.StatusId = 4;
            item.StatusChange = DateTime.Now;
            await dbContext.SaveChangesAsync();
        }

        public async Task AddProduct(string name, decimal price, int count, string warehouseCity)
        {
            if (!await dbContext.Products.AnyAsync(p => p.Name.ToLower() == name.ToLower()))
            {
                await CreateProduct(name, price);
            }
            await CreateItem(name, count, warehouseCity);
        }

        public async Task CreateProduct(string name, decimal price)
        {
            var product = new Product()
            {
                Items = new List<Item>(),
                Price = price,
                Name = name
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task CreateItem(string name, int count, string warehouseCity)
        {
            var product = await dbContext.Products.FirstAsync(p => p.Name == name);
            if (!await dbContext.Warehouses.AnyAsync(p => p.City.ToLower() == warehouseCity.ToLower()))
            {
                await CreateWarehouse(warehouseCity);
            }
            var warehouse = await dbContext.Warehouses.FirstOrDefaultAsync(p => p.City.ToLower() == warehouseCity.ToLower());
            for (int i = 0; i < count; i++)
            {
                var item = new Item()
                {
                    StatusChange = DateTime.Now,
                    StatusId = 1,
                    Warehouse = warehouse,
                    Product = product
                };
                await dbContext.Items.AddAsync(item);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task CreateWarehouse(string city)
        {
            var warehouse = new Warehouse()
            {
                City = city,
                Items = new List<Item>()
            };
            await dbContext.Warehouses.AddAsync(warehouse);
            await dbContext.SaveChangesAsync();
        }

        public async Task CheckFirstRun()
        {
            if ((await dbContext.Products.ToListAsync()).Count == 0)
                await InitialCreation();
        }

        public async Task InitialCreation()
        {
            await CreateWarehouse("Sofia");
            await CreateWarehouse("Varna");
            await CreateProduct("N.O.", 29.99m);
            await CreateItem("N.O.", 1, "Sofia");
            await CreateProduct("What?", 0.60m);
            await CreateItem("What?", 12, "Sofia");
            await CreateProduct("No Idea", 24.97m);
            await CreateItem("No Idea", 4, "Sofia");
            await CreateProduct("Car Key", 1.00m);
            await CreateItem("Car Key", 7, "Varna");
        }

    }
}
