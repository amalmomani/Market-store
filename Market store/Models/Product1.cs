using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_store.Models
{
    public partial class Product1
    {
        public Product1()
        {
            Orderproducts = new HashSet<Orderproduct>();
            Productshops = new HashSet<Productshop>();
        }

        public decimal Productid { get; set; }
        public string Productname { get; set; }
        public decimal? Price { get; set; }
        public decimal? Productvalue { get; set; }
        public string Image { get; set; }
        public string Productsize { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<Orderproduct> Orderproducts { get; set; }
        public virtual ICollection<Productshop> Productshops { get; set; }
    }
}
