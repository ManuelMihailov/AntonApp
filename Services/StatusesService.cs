using Data;
using Data.Contracts;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class StatusesService : IStatusesService
    {
        private readonly AntonDatabaseContext dbContext;
        public StatusesService(AntonDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CheckFirstRun()
        {
            if ((await dbContext.Statuses.ToListAsync()).Count == 0)
                await InitialCreation();
        }
        public async Task InitialCreation()
        {
            var status1 = new Status
            {
                Type = "In Stock",
                Items = new List<Item>()
            };
            await dbContext.Statuses.AddAsync(status1);
            await dbContext.SaveChangesAsync();
            var status2 = new Status
            {
                Type = "Pending",
                Items = new List<Item>()
            };
            await dbContext.Statuses.AddAsync(status2);
            await dbContext.SaveChangesAsync();
            var status3 = new Status
            {
                Type = "In Delivery",
                Items = new List<Item>()
            };
            await dbContext.Statuses.AddAsync(status3);
            await dbContext.SaveChangesAsync();
            var status4 = new Status
            {
                Type = "Completed",
                Items = new List<Item>()
            };
            await dbContext.Statuses.AddAsync(status4);
            await dbContext.SaveChangesAsync();
        }
    }
}
