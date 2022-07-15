using System;
using System.Collections.Generic;

namespace IdentityPractise.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CrearetAt { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
