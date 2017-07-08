using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using clean_aspnet_mvc.Data;
namespace clean_aspnet_mvc.Data
{

    public class EventMeal : BaseEntityChildOfLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public virtual Event Event { get; set; }

        [Required]
        [Display(Name = "Meal Slot")]
        public virtual EventMealSlotType EventMealSlot { get; set; }

    }
}
