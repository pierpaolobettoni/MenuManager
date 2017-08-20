using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clean_aspnet_mvc.Data;
using clean_aspnet_mvc.Data.Calculations;
using static clean_aspnet_mvc.Data.Calculations.MealItemMultiplier;

namespace clean_aspnet_mvc.Models.ShoppingViewModels
{
    public class ShoppingListCalculationViewModel
    {
        public MealsShoppingList ShoppingList { get; private set; }
        public CurrentLoggedInUser CurrentUser { get; private set; }
        public List<GroceryItem> AllIngredientsCache { get; private set; }
        public List<GroceryCategory> AllGroceryCategoriesCache { get; private set; }

        public List<GroceryItem> UsedIngredients { get; private set; }
        public List<GroceryCategory> UsedGroceryCategories { get; private set; }
        public List<Event> AllEventsCache { get; private set; }


        public List<EventMealSlotType> GetAllMealSlotsCache { get; private set; }
        public List<MealItemMultiplier> UsedMultipliers { get; private set; }
        public List<MealItemIngredient> UsedMealItemIngredients { get; private set; }

        public List<SingleRow> ShoppingListRows {get; private set;}

        public List<SingleRow> GetConsolidatedShoppingListRows()
        {
            List<SingleRow> retList = new List<SingleRow>();
            // get all the distinct grocery item IDs
            var groceryItemIds = ShoppingListRows.Select(x=> x.GroceryItemId).Distinct();
            foreach (int thisId in groceryItemIds)
            {
                // get all measures for that Id
                List<SingleRow> rowsWithSameId = ShoppingListRows.Where(x => x.GroceryItemId == thisId).ToList();
                var measures = rowsWithSameId.Select( x=> x.Measure).Distinct();

                foreach(string thisMeasure in measures)
                {
                    // make the total for each measure
                    decimal sumOfQuantity = rowsWithSameId.Where(x => x.Measure == thisMeasure).Sum( x=> x.Quantity);
                    retList.Add(new SingleRow(0, rowsWithSameId[0].GroceryItemName, rowsWithSameId[0].CategoryName, thisMeasure, sumOfQuantity));

                }
            }
            return retList;
        }


        public ShoppingListCalculationViewModel(MealsShoppingList shoppingList, CurrentLoggedInUser CurrentLoggedInUser)
        {
            ShoppingList = shoppingList;
            CurrentUser = CurrentLoggedInUser;
            HydrateViewModel();
        }

        public void HydrateViewModel()
        {
            GetCachedData();
            CalculateUsedMultipliers();
            CalculateUseMealItemIngredients();
            CalculcateIngredients();
            CalculatedUsedGroceryCategories();
            TotalsForEachGroceryItem totals = new TotalsForEachGroceryItem(UsedMultipliers);
            List<FlatIngredientCalculation> totalsWithIDs = totals.GetTotals();
            ShoppingListRows = new List<SingleRow>();
            foreach(var thisTotal in totalsWithIDs)
            {
                var thisGroceryItem = GetGroceryItemFromCache(thisTotal.GroceryItemId);
                var thisGroceryCategory = GetGroceryCategoryFromCache(thisGroceryItem.GroceryCategoryId);
                var newRow = new SingleRow(thisTotal, thisGroceryItem, thisGroceryCategory);
                ShoppingListRows.Add(newRow);
            }
        }

        private void GetCachedData()
        {
            // get all ingredients
            AllIngredientsCache = CurrentUser.GetAllGroceryItems().Result;
            // get all events
            var eventIds = ShoppingList.GetAllMeals().Select(x => x.EventId).Distinct().ToArray();
            AllEventsCache = CurrentUser.DBContext.Events.Where(x => eventIds.Contains(x.Id)).ToList();
            // get all eventMeals

            // get all GroceryCategories
            AllGroceryCategoriesCache = CurrentUser.GetGroceryCategories();
            // get all meal spots
            GetAllMealSlotsCache = CurrentUser.GetEventMealSlotTypes();
        }

        public void CalculatedUsedGroceryCategories()
        {
            var categoryIds = UsedIngredients.Select(x => x.GroceryCategoryId);
            UsedGroceryCategories = AllGroceryCategoriesCache.Where(x => categoryIds.Contains(x.Id)).ToList();
        }

        public void CalculcateIngredients()
        {
            var ingredientIds = UsedMealItemIngredients.Select(x => x.GroceryItemId);
            UsedIngredients = AllIngredientsCache.Where(x => ingredientIds.Contains(x.Id)).ToList();
        }

        public List<GroceryItem> GroceryItemsInCategory(int id)
        {
            return AllIngredientsCache.Where(x => x.GroceryCategoryId == id).OrderBy(x => x.GroceryItemName).ToList();
        }

        public Event GetEvent(int id)
        {
            return AllEventsCache.Where(x => x.Id == id).First();
        }

        public List<EventMeal> GetAllMeals()
        {
            return ShoppingList.GetAllMeals();
        }

        public void CalculateUsedMultipliers()
        {
            UsedMultipliers = ShoppingList.EventMealShoppingLists.SelectMany(x => x.MealItemMultiplier).ToList();
        }

        public void CalculateUseMealItemIngredients()
        {
            var allMealItemIds = this.UsedMultipliers.Select(x => x.MealItem.Id).Distinct().ToList();
            UsedMealItemIngredients = (from mii in this.CurrentUser.DBContext.MealItemIngredients
                                       where allMealItemIds.Contains(mii.MealItemId)
                                       select mii).ToList();
        }

        public EventMeal GetMeal(int id)
        {
            return GetAllMeals().Where(x => x.Id == id).First();
        }

        public GroceryCategory GetGroceryCategoryFromCache(int id)
        {
            return AllGroceryCategoriesCache.Where(x => x.Id == id).First();
        }

        public EventMealSlotType GetEventMealSlotTypeFromCache(int id)
        {
            return GetAllMealSlotsCache.Where(x => x.Id == id).First();
        }

        public GroceryItem GetGroceryItemFromCache(int id)
        {
            return AllIngredientsCache.Where(x=> x.Id == id).First();
        }

        // public List<MealItemMultiplier.FlatIngredientCalculation> GetTotalsForGroceryItem(int groceryItemId)
        // {
        //     // find allIngredient with said grocery id
        //     var relevantIngredients = UsedMealItemIngredients.Where(x => x.GroceryItemId == groceryItemId).ToArray();
        //     var allMealItemIds = relevantIngredients.Select(x => x.MealItemId).Distinct().ToArray();
        //     var relevantMultipliers = UsedMultipliers.Where(x => allMealItemIds.Contains(x.MealItem.Id)).ToArray();
        //     TotalsForEachGroceryItem totals = new TotalsForEachGroceryItem(relevantMultipliers);
        //     return totals.GetTotals();
        // }




        public class TotalsForEachGroceryItem
        {
            // all the relevant multipliers
            public List<MealItemMultiplier> Multipliers { get; private set; }


            // this is the calculation based on MealItemMultipliers
            public List<MealItemMultiplier.FlatIngredientCalculation> GetTotals()
            {
                List<MealItemMultiplier.FlatIngredientCalculation> result = Multipliers.SelectMany(x => x.CalculateIngredientQuantities()).ToList();
                return result;
            }

            public TotalsForEachGroceryItem(List<MealItemMultiplier> multipliers)
            {
                Multipliers = multipliers;
            }


        }

        public class SingleRow
        {


            public string GroceryItemName {get; private set;}

            public int GroceryItemId {get; private set;}

            public string CategoryName {get; private set;}

            public string Measure {get; private set;}

            public decimal Quantity {get; private set;}

            public SingleRow(FlatIngredientCalculation calculation, GroceryItem groceryItem, GroceryCategory groceryCategory)
            {
                GroceryItemName = groceryItem.GroceryItemName;
                GroceryItemId = groceryItem.Id;
                CategoryName = groceryCategory.GroceryCategoryName;
                Measure = calculation.Measure;
                Quantity = calculation.Quantity;
            }

            public SingleRow(int groceryItemId, string groceryItemName, string groceryCategoryName, string measure, decimal quantity)
            {
                GroceryItemName = groceryItemName;
                GroceryItemId = groceryItemId;
                CategoryName = groceryCategoryName;
                Measure = measure;
                Quantity = quantity;
            }
        }



    }
}
