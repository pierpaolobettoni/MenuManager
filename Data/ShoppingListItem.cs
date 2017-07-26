using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace clean_aspnet_mvc.Data
{

    public class ShoppingListItem
    {
        public GroceryItem GroceryItem { get; set; }

        public decimal Quantity { get; set; }

        public string Measure { get; set; }

    }

    public class MealItemShoppingListItem
    {

        public MealItemShoppingListItem()
        {
            ShoppingListItems = new List<ShoppingListItem>();
        }
        public MealItem MealItem { get; set; }

        public List<ShoppingListItem> ShoppingListItems { get; set; }


        public decimal Multiplier { get; set; }
    }

    public class EventMealShoppingList
    {
        public EventMealShoppingList()
        {
            MealItemShoppingLists = new List<MealItemShoppingListItem>();
        }
        public List<MealItemShoppingListItem> MealItemShoppingLists { get; set; }
    }

    public class MealsShoppingList
    {
        public MealsShoppingList()
        {
            MealShoppingLists = new List<EventMealShoppingList>();
        }
        public List<EventMealShoppingList> MealShoppingLists {get; set;}
    }

    public class ShoppingListCalculator
    {

        public static async Task<MealItemShoppingListItem> CalculateShoppingListForMealItem(MealItem mealItem, int numberOfPeople, ApplicationDbContext DBContext)
        {
            MealItemShoppingListItem retValue;
            // hydrate mealItem
            var hydratedMealItem = await DBContext.MealItems.Include("Ingredients").Include("MeasureType").Where(x => x.Id == mealItem.Id).FirstAsync();

            // calculate how many MealItems it takes to satisfy the number of people
            var multiplier = numberOfPeople % hydratedMealItem.NumberOfServings;

            // now that I have the meal item, go through the ingredient
            List<ShoppingListItem> listOfIngredients = new List<ShoppingListItem>();
            foreach (var thisIngredient in hydratedMealItem.Ingredients)
            {
                ShoppingListItem shoppingListItem = new ShoppingListItem()
                {
                    GroceryItem = thisIngredient.GroceryItem,
                    Quantity = thisIngredient.Quantity * multiplier,
                    Measure = thisIngredient.MeasureType
                };
                listOfIngredients.Add(shoppingListItem);

            }
            retValue = new MealItemShoppingListItem()
            {
                ShoppingListItems = listOfIngredients,
                MealItem = mealItem,
                Multiplier = multiplier
            };

            return retValue;
        }

        public static async Task<EventMealShoppingList> CalculateShoppingList(EventMeal meal, ApplicationDbContext DBContext)
        {
            EventMealShoppingList retValue = new EventMealShoppingList();
            var hydratedEventMeal = await DBContext.EventMeal.Include("Menu").Include("Menu.MealItems").Where(x => x.Id == meal.Id).FirstAsync();
            List<MealItemShoppingListItem> mealItemShoppingLists = new List<MealItemShoppingListItem>();
            // go through all the meal items
            foreach (var mealItem in hydratedEventMeal.Menu.MealItems)
            {
                var thisMealItemShoppingList = await CalculateShoppingListForMealItem(mealItem.MealItem, hydratedEventMeal.NumberOfPeopleAttending, DBContext);
                mealItemShoppingLists.Add(thisMealItemShoppingList);
            }
            retValue.MealItemShoppingLists = mealItemShoppingLists;
            return retValue;
        }

        public static async Task<MealsShoppingList> CalculateShoppingList(int[] mealIds, CurrentLoggedInUser currentUser)
        {
            MealsShoppingList retValue = new MealsShoppingList();
            var DBContext = currentUser.DBContext;
            // get the actual event Meals
            var meals = await DBContext.EventMeal.Where(x => x.Location == currentUser.GetCurrentLocation() && mealIds.Contains(x.Id)).ToListAsync();
            foreach(var thisMeal in meals)
            {
                var calculationResult = await CalculateShoppingList(thisMeal, DBContext);
                retValue.MealShoppingLists.Add(calculationResult);
            }
            return retValue;
        }


    }


}
