using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace clean_aspnet_mvc.Data
{

    public class MealItem : BaseEntityChildOfLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required]
        [Display(Name = "Meal Item")]
        public string MealItemName { get; set; }
        [Display(Name = "Description")]
        public string MealItemDescription { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }
        [Display(Name = "Measure Type")]
        public string MeasureType { get; set; }

        public ICollection<MealItemIngredient> Ingredients { get; set; }

        [ForeignKey("MenuItemType")]
        [Display(Name = "Meal Item Type")]
        [UIHint("MenuItemTypeId")]

        public int MenuItemTypeId {set; get;}

        public MenuItemType MenuItemType {get;set;}

        [Display(Name="Number of Servings")]
        [Required]
        public int NumberOfServings{get; set;}
    }

}

