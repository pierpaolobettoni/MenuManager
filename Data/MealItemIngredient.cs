using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace clean_aspnet_mvc.Data
{
    public class MealItemIngredient : BaseEntityChildOfLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }


        public GroceryItem GroceryItem { get; set; }

        [ForeignKey("GroceryItem")]
        [UIHint("GroceryItemId")]
        public int GroceryItemId { get; set; }

        [ForeignKey("MealItem")]
        public int MealItemId { get; set; }


        public MealItem MealItem { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string MeasureType { get; set; }



    }

}
