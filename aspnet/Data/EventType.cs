using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using clean_aspnet_mvc.Data;

namespace clean_aspnet_mvc.Data
{

public class EventType : BaseEntityChildOfLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public string EventTypeName { get; set; }

        public virtual ICollection<Event> Events {get; set;}
    }
}
