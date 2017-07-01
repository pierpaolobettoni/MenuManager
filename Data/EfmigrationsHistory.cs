using System;
using System.Collections.Generic;

namespace clean_aspnet_mvc.Data
{
    public partial class EfmigrationsHistory
    {
        public int id {get; set;}
        public string MigrationId { get; set; }
        public string ProductVersion { get; set; }
    }
}
