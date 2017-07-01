using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace clean_aspnet_mvc.Data
{
    public partial class ApplicationDbContext
    {
        public virtual DbSet<Locations> Locations { get; set; }

        public virtual DbSet<UserLocations> UserLocations { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<EventType> EventTypes { get; set; }

    }
}
