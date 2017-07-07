using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clean_aspnet_mvc.Data
{
    public partial class UserLocations
    {
        public UserLocations()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }

        [ForeignKey("Location")]
        public int LocationId {get;set;}
        public Locations Location { get; set; }

        public bool IsDefaultLocationForUser {get; set;}
       }
}
