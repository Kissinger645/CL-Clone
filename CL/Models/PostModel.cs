using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CL.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Posted { get; set; }
        public int Price { get; set; }

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual ApplicationUser ThisUser { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual LocationModel City { get; set; }

        public int CatId { get; set; }

        [ForeignKey("CatId")]
        public virtual CatModel Category { get; set; }
    }
}