using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Models.User
{
    public class UserPrivateModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Bio { get; set; }
        public bool DisplayContactInfo { get; set; }
    }
}
