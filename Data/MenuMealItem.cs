using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using clean_aspnet_mvc.Data;
namespace clean_aspnet_mvc.Data
{

    public class MenuMealItem : BaseEntityChildOfLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Meal Item")]
        public virtual MealItem MealItem { get; set; }

        [ForeignKey("MealItem")]
        public int MealItemId {get; set;}
        public virtual Menu Menu{get; set;}
        [ForeignKey("Menu")]
        public int MenuId {get; set;}
    }
}
