using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clean_aspnet_mvc.Data
{
    public partial class Locations
    {
        public Locations()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string LocationName { get; set; }
        public string Description { get; set; }

        public ICollection<UserLocations> UserLocations { get; set; }
    }
}
