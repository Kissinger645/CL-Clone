using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CL.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ApplicationUser ThisUser { get; set; }
        public virtual LocationModel City { get; set; }
        public virtual CatModel Category { get; set; }
    }
}