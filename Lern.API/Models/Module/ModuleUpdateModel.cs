using Lern.API.Models.Course;
using Lern.API.Models.Lesson;
using Lern.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models.Module
{
    public class ModuleUpdateModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool? IsPublic { get; set; }
    }
}
