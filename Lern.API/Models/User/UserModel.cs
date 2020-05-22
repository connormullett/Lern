using System;

namespace Lern.API.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Bio { get; set; }
        public string PhoneNumber { get; set; }
        public bool DisplayContactInfo { get; set; }
    }
}