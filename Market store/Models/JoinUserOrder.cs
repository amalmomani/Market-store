using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market_store.Models
{
    public class JoinUserOrder
    {
        public Orderproduct orderproduct { get; set; }
        public Useraccount useraccount { get; set; }
        public Order1 order { get; set; }
        public Product1 product { get; set; }
        public Productshop productshop { get; set; }
        public Shop1 shop { get; set; }
    }
}
