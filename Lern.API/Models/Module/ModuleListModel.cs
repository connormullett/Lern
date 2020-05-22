using Lern.API.Models.Course;
using Lern.API.Models.Lesson;
using Lern.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models.Module
{
    public class ModuleListModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }

        public int CourseId { get; set; }
        public CourseModel Course { get; set; }
    }
}
