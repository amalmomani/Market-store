﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_store.Models
{
    public partial class Aboutu
    {
        public decimal Aboutid { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
