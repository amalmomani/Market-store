using System;
using System.Collections.Generic;

#nullable disable

namespace Market_store.Models
{
    public partial class Orderproduct
    {
        public decimal Id { get; set; }
        public decimal? Orderid { get; set; }
        public decimal? Numberofpieces { get; set; }
        public decimal? Totalamount { get; set; }
        public string Status { get; set; }
        public decimal? Productid { get; set; }
        public decimal? Shopid { get; set; }

        public virtual Order1 Order { get; set; }
        public virtual Product1 Product { get; set; }
    }
}
