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
    public class AccountService : IAccountService
    {
        private readonly AntonDatabaseContext dbContext;
        private readonly IHasher hasher;
        public AccountService(AntonDatabaseContext dbContext, IHasher hasher)
        {
            this.hasher = hasher;
            this.dbContext = dbContext;
        }

        public async Task AddAccountAsync(string username, string password, string city)
        {
            if (String.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be null or empty.");
            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.");
            if (await this.dbContext.Users.AnyAsync(p => p.Username == username))
                throw new ArgumentException("Username is already taken.");

            var user = new User()
            {
                Username = username,
                Password = hasher.Hash(password),
                City = city,
                AccountType = "User",
                Requests = new List<Request>()
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        } 
        public async Task<User> AttemptUserLoginAsync(string username, string password)
        {
            var user = await dbContext.Users
               .Where(p => p.Username == username)
               .FirstOrDefaultAsync();
            if (user == null || !this.hasher.Verify(password, user.Password))
            {
                throw new ArgumentException("username or password incorrect");
            }
            return user;
        }

        public async Task CreateTestAdmin()
        {
            var user = new User()
            {
                Username = "Admin",
                Password = hasher.Hash("123321"),
                City = "Sofia",
                AccountType = "Admin",
                Requests = new List<Request>()
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
