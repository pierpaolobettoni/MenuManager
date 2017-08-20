using System;
using System.Collections.Generic;
using clean_aspnet_mvc.Models;

namespace clean_aspnet_mvc.Data
{
    public partial class AspNetUserLogins
    {
        public int id {get; set;}
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
