using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_store.Models
{
    public partial class Mainpage
    {
        public decimal Homeid { get; set; }
        public string Companylogo { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Companyemail { get; set; }
        public string Companyphone { get; set; }
    }
}
