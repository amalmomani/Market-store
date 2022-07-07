using System;
using System.Collections.Generic;

#nullable disable

namespace Market_store.Models
{
    public partial class Order1
    {
        public Order1()
        {
            Orderproducts = new HashSet<Orderproduct>();
        }

        public decimal Orderid { get; set; }
        public decimal? Userid { get; set; }
        public DateTime? Orderdate { get; set; }
        public string Status { get; set; }

        public virtual Useraccount User { get; set; }
        public virtual ICollection<Orderproduct> Orderproducts { get; set; }
    }
}
