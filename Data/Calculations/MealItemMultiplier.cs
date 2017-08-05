using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        public List<FlatIngredientCalculation> CalculateIngredientQuantities()
        {
            List<FlatIngredientCalculation> calculations = new List<FlatIngredientCalculation>();
            MealItem.Ingredients.ToList().ForEach( x => {
                FlatIngredientCalculation newCalculation = new FlatIngredientCalculation();
                newCalculation.GroceryItemId = x.GroceryItemId;
                newCalculation.Measure = x.MeasureType.ToLower();
                newCalculation.Quantity = x.Quantity * Multiplier;
                calculations.Add(newCalculation);
            });
            return calculations;

        }

        public class FlatIngredientCalculation
        {
            public int GroceryItemId {get; set;}
            public decimal Quantity {get; set;}

            public string Measure {get; set;}
        }


    }


}
