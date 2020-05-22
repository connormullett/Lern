using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models.Lesson
{
    public class LessonCreateModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public bool IsPublic { get; set; } = false;
    }
}
