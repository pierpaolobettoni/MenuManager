using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clean_aspnet_mvc.Data;
using clean_aspnet_mvc.Models.ShoppingViewModels;

namespace clean_aspnet_mvc.Controllers
{
    public class ShopController : ControllerBase
    {
        public ShopController(ApplicationDbContext context)
        : base(context)
        {


        }

        public async Task<IActionResult> CalculateIngredients(int id)
        {
            // show all future events
            Event[] nextEvents = null;
            if (id <= 0)
            {
               nextEvents = await DBContext.Events
                .Include("Meals")
                .Include("Meals.Menu")
                .Include("Meals.EventMealSlot")
                .Include("Meals.Event")
                    .Where(x => x.StartDate >= DateTime.Today && x.Location == GetLoggedInUser().GetCurrentLocation())
                    .OrderBy(x => x.StartDate)
                    .ThenBy(x => x.EventName)
                    .ToArrayAsync();

            }
            else
            {
                 nextEvents = await DBContext.Events
                    .Include(x => x.Meals)
                    .Include("Meals.Menu")
                    .Include("Meals.EventMealSlot")
                    .Include("Meals.Event")
                    .Where(x => x.Id == id && x.Location == GetLoggedInUser().GetCurrentLocation())
                    .OrderBy(x => x.StartDate).ThenBy(x => x.EventName)
                    .ToArrayAsync();

            }
             return View(nextEvents);
        }

        [HttpPost]
        public async Task<IActionResult> CalculateIngredientsForMeals(int[] selectedMealIds)
        {

            var mealShoppingList = await base.GetLoggedInUser().CalculateShoppingList(selectedMealIds);
            var meals = await base.DBContext.EventMeal.Include(x=> x.Event).Include(x => x.Menu).Include(x => x.EventMealSlot).Where(x => selectedMealIds.Contains(x.Id)).ToListAsync();


            ViewBag.Meals = meals;

            var model = new ShoppingListCalculationViewModel(mealShoppingList, base.GetLoggedInUser());
            return View(model);
        }


    }
}
