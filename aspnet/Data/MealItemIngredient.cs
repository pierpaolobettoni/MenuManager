using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace clean_aspnet_mvc.Data
{
    public class MealItemIngredient : BaseEntityChildOfLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Display(Name = "Grocery Item")]
        public GroceryItem GroceryItem { get; set; }

        [ForeignKey("GroceryItem")]
        [UIHint("GroceryItemId")]
        [Display(Name = "Grocery Item")]
        public int GroceryItemId { get; set; }

        [ForeignKey("MealItem")]
        [Display(Name = "Meal Item")]
        public int MealItemId { get; set; }

        [Display(Name = "Meal Item")]
        public MealItem MealItem { get; set; }

        [Required]

        public decimal Quantity { get; set; }
        [Display(Name = "Measure Type")]
        public string MeasureType { get; set; }



    }

}
