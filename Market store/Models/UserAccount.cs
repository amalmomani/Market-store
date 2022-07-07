using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Market_store.Models
{
    public partial class Useraccount
    {
        public Useraccount()
        {
            Order1s = new HashSet<Order1>();
            Payments = new HashSet<Payment>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal Userid { get; set; }
        public string Fullname { get; set; }
        public decimal? Phonenumber { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal? Roleid { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Order1> Order1s { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
