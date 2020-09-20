using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contracts
{
    public interface IStatus
    {
        int Id { get; set; }
        string Type { get; set; }
        ICollection<Item> Items { get; set; }
    }
}
