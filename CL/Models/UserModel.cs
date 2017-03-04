using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CL.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Username { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual LocationModel Location { get; set; }
    }
}