using System.Collections.Generic;
using clean_aspnet_mvc.Data;

namespace clean_aspnet_mvc.Models.LocationsList
{
    public class LocationsListViewModel
    {

        public ICollection<Locations> LocationsList { get; set; }

        public int? DefaultLocationId { get; set; }

    }
}
