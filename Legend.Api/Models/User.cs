using System;
using System.Collections.Generic;

namespace Legend.Api.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserAccount { get; set; }
        public string UserPassword { get; set; }
        public string UserPaswordMd5 { get; set; }
    }
}
