using System.Collections.Generic;

namespace clean_aspnet_mvc.Data.Calculations
{
    public class EventMealShoppingList
    {
        public EventMealShoppingList(EventMeal eventMeal, List<MealItemMultiplier> multipliers)
        {
            MealItemMultiplier = multipliers;
            EventMeal= eventMeal;
        }
        public List<MealItemMultiplier> MealItemMultiplier { get; private set; }
        public EventMeal EventMeal {get; private set;}
    }


}
