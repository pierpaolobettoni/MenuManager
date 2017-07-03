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
        public string EventName { get; set; }
        [DataType(DataType.MultilineText)]
        public string EventDescription {get;set;}
        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [ForeignKey("EventType")]
        [UIHint("EventTypeId")]
        public int EventTypeId { get; set; }

        public virtual EventType EventType { get; set; }



    }




}
