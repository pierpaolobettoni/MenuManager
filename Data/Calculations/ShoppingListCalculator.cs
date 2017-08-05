using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace clean_aspnet_mvc.Data.Calculations
{
    public class ShoppingListCalculator
    {
        private static MealItemMultiplier CalculateMealItemMultiplier(MealItem mealItem, int numberOfPeople)
        {
            // calculate how many MealItems it takes to satisfy the number of people
            var multiplier = Math.Ceiling( (decimal)numberOfPeople / (decimal)mealItem.NumberOfServings);
            return new MealItemMultiplier(mealItem, multiplier);
        }

        private static EventMealShoppingList CalculateShoppingList(EventMeal eventMeal, ApplicationDbContext DBContext)
        {

            var hydratedEventMeal = DBContext.EventMeal.Include( x=> x.Menu.MealItems).Where(x => x.Id == eventMeal.Id).First();
            List<MealItemMultiplier> mealItemShoppingLists = new List<MealItemMultiplier>();
            var allMealItemIds = hydratedEventMeal.Menu.MealItems.Select( x=> x.MealItemId).ToArray();
            var allmealItems = DBContext.MealItems.Include("Ingredients").Where( x => allMealItemIds.Contains(x.Id)).ToArray();
            // go through all the meal items
            Parallel.ForEach (allmealItems,  thisMenuMealItem =>
            {
                var mealItemMultiplier = CalculateMealItemMultiplier(thisMenuMealItem, hydratedEventMeal.NumberOfPeopleAttending);
               mealItemShoppingLists.Add(mealItemMultiplier);
            });
            EventMealShoppingList retValue = new EventMealShoppingList(hydratedEventMeal, mealItemShoppingLists);
            return retValue;
        }

        public static async Task<MealsShoppingList> CalculateShoppingList(int[] eventMealIds, CurrentLoggedInUser currentUser)
        {

            var DBContext = currentUser.DBContext;
            // get the actual event Meals
            var meals = await DBContext.EventMeal.Where(x => x.Location == currentUser.GetCurrentLocation() && eventMealIds.Contains(x.Id)).ToListAsync();
            List<EventMealShoppingList> shoppingLists = new List<EventMealShoppingList>();
            foreach (EventMeal thisEventMeal in meals)
            {
                var calculationResult = CalculateShoppingList(thisEventMeal, DBContext);
                shoppingLists.Add(calculationResult);
            }
            MealsShoppingList retValue = new MealsShoppingList(shoppingLists);
            return retValue;
        }



    }


}
