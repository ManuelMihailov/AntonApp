using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAccountService
    {
        Task CreateTestAdmin();
        Task AddAccountAsync(string email, string password, string city);
        Task<User> AttemptUserLoginAsync(string email, string password);
    }
}
