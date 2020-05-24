using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models.Posts
{
    public class PostCreateModel
    {
        [Required]
        public string Title { get; set; }

        public string Body { get; set; }

        public bool IsPublic { get; set; }
    }
}
