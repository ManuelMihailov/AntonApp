using Data;
using Data.Contracts;
using Data.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class StatusesService : IStatusesService
    {
        private readonly AntonDatabaseContext dbContext;
        public StatusesService(AntonDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void CheckFirstRun()
        {
            if (dbContext.Statuses.ToList().Count == 0)
                InitialCreation();
        }
        public void InitialCreation()
        {
            var status1 = new Status
            {
                Type = "In Stock",
                Items = new List<Item>()
            };
            dbContext.Statuses.Add(status1);
            dbContext.SaveChanges();
            var status2 = new Status
            {
                Type = "In Delivery",
                Items = new List<Item>()
            };
            dbContext.Statuses.Add(status2);
            dbContext.SaveChanges();
            var status3 = new Status
            {
                Type = "Completed",
                Items = new List<Item>()
            };
            dbContext.Statuses.Add(status3);
            dbContext.SaveChanges();
        }
    }
}
