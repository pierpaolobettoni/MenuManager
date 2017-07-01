using System;
using System.Collections.Generic;

namespace clean_aspnet_mvc.Data
{
    public partial class AspNetUserTokens
    {
        public int id {get; set;}
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
