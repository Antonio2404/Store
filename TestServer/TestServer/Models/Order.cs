using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestServer.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Position> Positions { get; set; }

    }
}
