using System;
using System.Collections.Generic;

#nullable disable

namespace Market_store.Models
{
    public partial class Payment
    {
        public decimal Payid { get; set; }
        public decimal? Cardnumber { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Paydate { get; set; }
        public decimal? Userid { get; set; }

        public virtual Useraccount User { get; set; }
    }
}
