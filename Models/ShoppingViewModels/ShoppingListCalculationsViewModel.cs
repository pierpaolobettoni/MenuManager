using clean_aspnet_mvc.Data.Calculations;

namespace clean_aspnet_mvc.Models.ShoppingViewModels
{
    public class ShoppingListCalculationViewModel
    {
        public MealsShoppingList ShoppingList { get; private set; }

        public ShoppingListCalculationViewModel(MealsShoppingList shoppingList, CurrentLoggedInUser CurrentLoggedInUser)
        {
            ShoppingList = shoppingList;
        }
    }
}
