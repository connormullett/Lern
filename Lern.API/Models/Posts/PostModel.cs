using Lern.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models.Posts
{
    public class PostModel
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public UserModel User { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool IsPublic { get; set; }
    }
}
