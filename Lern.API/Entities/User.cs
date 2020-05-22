using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Email { get; set; }

        public ICollection<Course> Courses { get; set; }

        public ICollection<Module> Modules { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Bio { get; set; }

        public DateTime? LastLogin { get; set; }

        public string PhoneNumber { get; set; }

        public bool DisplayContactInfo { get; set; }
    }
}
