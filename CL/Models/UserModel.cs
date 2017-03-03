using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public virtual ApplicationUser Username { get; set; }
        public virtual LocationModel Location { get; set; }
    }
}