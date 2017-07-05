using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace clean_aspnet_mvc.Data
{
    public partial class ApplicationDbContext
    {
        public virtual DbSet<Locations> Locations { get; set; }

        public virtual DbSet<UserLocations> UserLocations { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<EventType> EventTypes { get; set; }

        public virtual DbSet<MealItem> MealItems { get; set; }

        public virtual DbSet<MealItemIngredient> MealItemIngredients { get; set; }

        public virtual DbSet<GroceryItem> GroceryItems { get; set; }

        public virtual DbSet<GroceryCategory> GroceryCategory { get; set; }

        public virtual DbSet<EventMealSlotType> EventMealSlotTypes { get; set; }

        public virtual DbSet<EventMeal> EventMeal { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

         public virtual DbSet<MenuMealItem> MenuMealItem { get; set; }

        public override int SaveChanges()
        {

            AddLocation(null);
            return base.SaveChanges();
        }

        public int SaveChanges(Locations location)
        {
            AddLocation(location);
            return SaveChanges();
        }

        public async Task<int> SaveChangesAsync(Locations location)
        {
            AddLocation(location);
            return await base.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddLocation(null);
            return await base.SaveChangesAsync();
        }

        private void AddLocation(Locations location)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntityChildOfLocation && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var entityType = entity.Entity.GetType().FullName;
                if (location == null)
                {
                    throw new ArgumentNullException(string.Format("Trying to save {0} but there's no location passed in", entityType));
                }
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntityChildOfLocation)entity.Entity).Location = location;
                }


            }
        }

    }
}
