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



    }
}
