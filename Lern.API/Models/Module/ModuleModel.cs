using Lern.API.Models.Course;
using Lern.API.Models.Lesson;
using Lern.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models.Module
{
    public class ModuleModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }

        public int CourseId { get; set; }
        public CourseModel Course { get; set; }

        public ICollection<LessonModel> Lessons { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool IsPublic { get; set; }
    }
}
