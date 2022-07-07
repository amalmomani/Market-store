using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_store.Models
{
    public partial class Shop1
    {
        public Shop1()
        {
            Productshops = new HashSet<Productshop>();
        }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public decimal Shopid { get; set; }
        public string Shopname { get; set; }
        public decimal? Categoryid { get; set; }
        public string Image { get; set; }
        public decimal? Totalsales { get; set; }
        public decimal? Monthlyrent { get; set; }

        public virtual Category1 Category { get; set; }
        public virtual ICollection<Productshop> Productshops { get; set; }
    }
}
