using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Entities
{
    public class Link
    {
        public Link()
        {
            this.Courses = new HashSet<Course>();
            this.Modules = new HashSet<Module>();
            this.Lessons = new HashSet<Lesson>();
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public string HyperLink { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Module> Modules { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
