using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Entities
{
    public class CourseLink
    {
        public int LinkId { get; set; }

        public Link Link { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
