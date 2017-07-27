using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clean_aspnet_mvc.Data;
using clean_aspnet_mvc.Data.Calculations;

namespace clean_aspnet_mvc.Models.ShoppingViewModels
{
    public class ShoppingListCalculationViewModel
    {
        public MealsShoppingList ShoppingList { get; private set; }
        public CurrentLoggedInUser CurrentUser { get; private set; }
        public List<GroceryItem> AllIngredients { get; private set; }
        public List<Event> AllEvents { get; private set; }

        public ShoppingListCalculationViewModel(MealsShoppingList shoppingList, CurrentLoggedInUser CurrentLoggedInUser)
        {
            ShoppingList = shoppingList;
            CurrentUser = CurrentLoggedInUser;
        }

        public async Task HydrateViewModel()
        {
            // get all ingredients
            AllIngredients = await CurrentUser.GetAllGroceryItems();
            // get all events
            var eventIds = ShoppingList.GetAllMeals().Select(x => x.EventId).ToArray();
            AllEvents = CurrentUser.DBContext.Events.Where( x => eventIds.Contains( x.Id)).ToList();
            // get all GroceryCategories
            AllGroceryCategories = CurrentUser.
            // get all meal spots
        }
    }
}
