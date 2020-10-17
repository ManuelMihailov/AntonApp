using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public ICollection<Request> Requests { get; set; }
        public string AccountType { get; set; }
    }
}
