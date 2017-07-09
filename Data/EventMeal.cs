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

        [ForeignKey("Event")]
        public int EventId {get;set;}

        public virtual Event Event { get; set; }

        [Required]
        [Display(Name = "Meal Slot")]
        public virtual EventMealSlotType EventMealSlot { get; set; }

        [ForeignKey("EventMealSlot")]
        public int EventMealSlotId {get; set;}

        [ForeignKey("Menu")]
        public int MenuId {get; set;}

        public Menu Menu {get;set;}
    }
}
