using System;
using System.Collections.Generic;
using clean_aspnet_mvc.Models;

namespace clean_aspnet_mvc.Data
{
    public partial class AspNetUserClaims
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
