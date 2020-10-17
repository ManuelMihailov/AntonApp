using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class Request
    {
        public int Id { get; set; }
        public byte status { get; set; }
        public Product Product { get; set; }
        public int? ProductId { get; set; }
        public Item Item { get; set; }
        public int? ItemID { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }

    }
}
