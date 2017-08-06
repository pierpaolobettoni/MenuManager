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

        [Display(Name = "Type of Serving")]
        public string TypeOfServing { get; set; }

        public ICollection<MealItemIngredient> Ingredients { get; set; }

        [ForeignKey("MenuItemType")]
        [Display(Name = "Meal Item Type")]
        [UIHint("MenuItemTypeId")]

        public int MenuItemTypeId { set; get; }

        public MenuItemType MenuItemType { get; set; }

        [Display(Name = "Number of Servings")]
        [Required]
        public int NumberOfServings { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                var fullName = MealItemName;
                if (this.MenuItemType != null)
                {
                    fullName = string.Format("{0}: {1}", MenuItemType.Name, MealItemName);
                }
                return fullName;
            }

        }
    }

}

