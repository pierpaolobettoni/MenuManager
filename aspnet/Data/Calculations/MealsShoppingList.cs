using System.Collections.Generic;
using System.Linq;

namespace clean_aspnet_mvc.Data.Calculations
{
    public class MealsShoppingList
    {
        public MealsShoppingList(List<EventMealShoppingList> shoppingLists)
        {
            EventMealShoppingLists = shoppingLists;
        }
        public List<EventMealShoppingList> EventMealShoppingLists {get; private set;}

        public List<EventMeal> GetAllMeals()
        {
            return EventMealShoppingLists.Select(x => x.EventMeal).Distinct().ToList();
        }




    }


}
