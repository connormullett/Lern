using Lern.API.Models.Module;
using Lern.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models.Lesson
{
    public class LessonModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int UserId { get; set; }

        public int ModuleId { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool IsPublic { get; set; }
    }
}
