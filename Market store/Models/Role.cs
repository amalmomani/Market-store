using System;
using System.Collections.Generic;

#nullable disable

namespace Market_store.Models
{
    public partial class Role
    {
        public Role()
        {
            Useraccounts = new HashSet<Useraccount>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<Useraccount> Useraccounts { get; set; }
    }
}
