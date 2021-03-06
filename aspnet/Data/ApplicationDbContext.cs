﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using clean_aspnet_mvc.Models;
using clean_aspnet_mvc.Models.MenuManagerViewModels;
using clean_aspnet_mvc.Data;

namespace clean_aspnet_mvc.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


        }

        public DbSet<clean_aspnet_mvc.Data.EventMealSlotType> EventMealSlotType { get; set; }

        public DbSet<clean_aspnet_mvc.Data.MenuItemType> MenuItemType { get; set; }

    }
}
