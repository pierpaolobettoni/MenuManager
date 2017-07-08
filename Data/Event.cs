using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace clean_aspnet_mvc.Data
{

    public class Event : BaseEntityChildOfLocation
    {
        public Event()
        {


        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Event Description")]
        public string EventDescription {get;set;}
        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [ForeignKey("EventType")]
        [UIHint("EventTypeId")]
        [Display(Name = "Event Type")]
        public int EventTypeId { get; set; }

        public virtual EventType EventType { get; set; }

        [Display(Name = "Event Meals")]
        public virtual ICollection<EventMeal> Meals {get; set;}



    }




}
