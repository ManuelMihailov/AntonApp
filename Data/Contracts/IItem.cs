using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contracts
{
    public interface IItem
    {
        int Id { get; set; }
        int ProductId { get; set; }
        Product Product { get; set; }
        int StatusId { get; set; }
        Status Status { get; set; }
        DateTime StatusChange { get; set; }
    }
}
