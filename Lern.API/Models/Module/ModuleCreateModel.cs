using Lern.API.Models.Course;
using Lern.API.Models.Lesson;
using Lern.API.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models
{
    public class ModuleCreateModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int CourseId { get; set; }

        public bool IsPublic { get; set; } = false;
    }
}
