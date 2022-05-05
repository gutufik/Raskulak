using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Order
    {
        public User User { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
