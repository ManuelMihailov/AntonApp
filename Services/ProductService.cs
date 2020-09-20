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
            var items = await dbContext.Items.Include(p => p.Product).ToListAsync();
            return items;
        }

        public async Task BuyItem(string name)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
            var item = await dbContext.Items.Where(p => p.ProductId == product.Id).FirstOrDefaultAsync();
            item.StatusId = 2;
            item.StatusChange = DateTime.Now;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeliverItem(int id)
        {
            var item = await dbContext.Items.Where(p => p.Id == id).FirstOrDefaultAsync();
            item.StatusId = 3;
            item.StatusChange = DateTime.Now;
            await dbContext.SaveChangesAsync();
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

        public async Task CreateItem(string name, int count)
        {
            var product = await dbContext.Products.FirstAsync(p => p.Name == name);
            for (int i = 0; i < count; i++)
            {
                var item = new Item()
                {
                    StatusChange = DateTime.Now,
                    StatusId = 1,
                    Product = product
                };
                await dbContext.Items.AddAsync(item);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task CheckFirstRun()
        {
            if (dbContext.Products.ToList().Count == 0)
                await InitialCreation();
        }

        public async Task InitialCreation()
        {
            await CreateProduct("N.O.", 29.99m);
            await CreateItem("N.O.", 1);
            await CreateProduct("What?", 0.60m);
            await CreateItem("What?", 12);
            await CreateProduct("No Idea", 24.97m);
            await CreateItem("No Idea", 4);
            await CreateProduct("Car Key", 1.00m);
            await CreateItem("Car Key", 7);
        }
    }
}
