using System;
using System.Collections.Generic;
using clean_aspnet_mvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace clean_aspnet_mvc.Data
{
    public partial class AspNetUserRoles
    {

        public int id {get; set;}
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual IdentityRole Role { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
