using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models.Posts
{
    public class PostUpdateModel
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public bool? IsPublic { get; set; }
    }
}
