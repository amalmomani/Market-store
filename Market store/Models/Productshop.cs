using System;
using System.Collections.Generic;

#nullable disable

namespace Market_store.Models
{
    public partial class Productshop
    {
        public decimal Id { get; set; }
        public decimal? Productid { get; set; }
        public decimal? Shopid { get; set; }

        public virtual Product1 Product { get; set; }
        public virtual Shop1 Shop { get; set; }
    }
}
