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

        private static EventMealShoppingList CalculateShoppingList(EventMeal meal, ApplicationDbContext DBContext)
        {

            var hydratedEventMeal = DBContext.EventMeal.Include( x=> x.Menu.MealItems).Where(x => x.Id == meal.Id).First();
            List<MealItemMultiplier> mealItemShoppingLists = new List<MealItemMultiplier>();
            // go through all the meal items
            Parallel.ForEach (hydratedEventMeal.Menu.MealItems,  thisMenuMealItem =>
            {
                // ugly way of doing it...
                var mealItem = DBContext.MealItems.Where( x => x.Id == thisMenuMealItem.MealItemId).First();
                var mealItemMultiplier = CalculateMealItemMultiplier(mealItem, hydratedEventMeal.NumberOfPeopleAttending);
               mealItemShoppingLists.Add(mealItemMultiplier);
            });
            EventMealShoppingList retValue = new EventMealShoppingList(hydratedEventMeal, mealItemShoppingLists);
            return retValue;
        }

        public static async Task<MealsShoppingList> CalculateShoppingList(int[] mealIds, CurrentLoggedInUser currentUser)
        {

            var DBContext = currentUser.DBContext;
            // get the actual event Meals
            var meals = await DBContext.EventMeal.Where(x => x.Location == currentUser.GetCurrentLocation() && mealIds.Contains(x.Id)).ToListAsync();
            List<EventMealShoppingList> shoppingLists = new List<EventMealShoppingList>();
            foreach (var thisMeal in meals)
            {
                var calculationResult = CalculateShoppingList(thisMeal, DBContext);
                shoppingLists.Add(calculationResult);
            }
            MealsShoppingList retValue = new MealsShoppingList(shoppingLists);
            return retValue;
        }

        public static async Task GetLookupDataForShoppingList(MealsShoppingList mealsShoppingList, ApplicationDbContext DBContext)
        {
            var allEventMeals = new List<object>();

            //var ingredientsInShoppingList = await DBContext.MealItemIngredients.Where( )
        }


    }


}
