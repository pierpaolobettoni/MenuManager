using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using clean_aspnet_mvc.Data;

namespace clean_aspnet_mvc.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170709055659_Added Menu to EventMeal")]
    partial class AddedMenutoEventMeal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("clean_aspnet_mvc.Data.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("Date");

                    b.Property<string>("EventDescription");

                    b.Property<string>("EventName")
                        .IsRequired();

                    b.Property<int>("EventTypeId");

                    b.Property<int?>("LocationId");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.EventMeal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventId");

                    b.Property<int>("EventMealSlotId");

                    b.Property<int?>("LocationId");

                    b.Property<int>("MenuId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("EventMealSlotId");

                    b.HasIndex("LocationId");

                    b.HasIndex("MenuId");

                    b.ToTable("EventMeal");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.EventMealSlotType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("EventMealSlotType");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EventTypeName")
                        .IsRequired();

                    b.Property<int?>("LocationId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.GroceryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroceryCategoryId");

                    b.Property<string>("GroceryItemName")
                        .IsRequired();

                    b.Property<int?>("LocationId");

                    b.HasKey("Id");

                    b.HasIndex("GroceryCategoryId");

                    b.HasIndex("LocationId");

                    b.ToTable("GroceryItems");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.Locations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("LocationName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.MealItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LocationId");

                    b.Property<string>("MealItemDescription");

                    b.Property<string>("MealItemName")
                        .IsRequired();

                    b.Property<string>("MeasureType");

                    b.Property<int>("MenuItemTypeId");

                    b.Property<int>("NumberOfServings");

                    b.Property<decimal>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("MenuItemTypeId");

                    b.ToTable("MealItems");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.MealItemIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroceryItemId");

                    b.Property<int?>("LocationId");

                    b.Property<int>("MealItemId");

                    b.Property<string>("MeasureType");

                    b.Property<decimal>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("GroceryItemId");

                    b.HasIndex("LocationId");

                    b.HasIndex("MealItemId");

                    b.ToTable("MealItemIngredients");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.MenuItemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("MenuItemType");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.MenuMealItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LocationId");

                    b.Property<int>("MealItemId");

                    b.Property<int>("MenuId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("MealItemId");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuMealItem");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.UserLocations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDefaultLocationForUser");

                    b.Property<int>("LocationId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("UserLocations");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("GroceryCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GroceryCategoryDescription");

                    b.Property<string>("GroceryCategoryName")
                        .IsRequired();

                    b.Property<int?>("LocationId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("GroceryCategory");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.Event", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.EventMeal", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.Event", "Event")
                        .WithMany("Meals")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("clean_aspnet_mvc.Data.EventMealSlotType", "EventMealSlot")
                        .WithMany()
                        .HasForeignKey("EventMealSlotId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("clean_aspnet_mvc.Data.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.EventMealSlotType", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.EventType", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.GroceryItem", b =>
                {
                    b.HasOne("GroceryCategory", "GroceryCategory")
                        .WithMany()
                        .HasForeignKey("GroceryCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.MealItem", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("clean_aspnet_mvc.Data.MenuItemType", "MenuItemType")
                        .WithMany()
                        .HasForeignKey("MenuItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.MealItemIngredient", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.GroceryItem", "GroceryItem")
                        .WithMany()
                        .HasForeignKey("GroceryItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("clean_aspnet_mvc.Data.MealItem", "MealItem")
                        .WithMany("Ingredients")
                        .HasForeignKey("MealItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.Menu", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.MenuItemType", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.MenuMealItem", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("clean_aspnet_mvc.Data.MealItem", "MealItem")
                        .WithMany()
                        .HasForeignKey("MealItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("clean_aspnet_mvc.Data.Menu", "Menu")
                        .WithMany("MealItems")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("clean_aspnet_mvc.Data.UserLocations", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany("UserLocations")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GroceryCategory", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Data.Locations", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("clean_aspnet_mvc.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("clean_aspnet_mvc.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
