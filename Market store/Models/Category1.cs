using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_store.Models
{
    public partial class Category1
    {
        public Category1()
        {
            Shop1s = new HashSet<Shop1>();
        }

        public decimal Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<Shop1> Shop1s { get; set; }
    }
}
