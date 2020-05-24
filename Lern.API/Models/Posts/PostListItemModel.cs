using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models.Posts
{
    public class PostListItemModel
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public bool IsPublic { get; set; }
    }
}
