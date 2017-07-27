using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clean_aspnet_mvc.Data.Calculations
{

    public class MealItemMultiplier
    {

        public MealItemMultiplier(MealItem mealItem, decimal multiplier)
        {
            MealItem = mealItem;
            Multiplier = multiplier;
        }
        public MealItem MealItem { get; private set; }

        public decimal Multiplier { get; private set; }



    }


}
