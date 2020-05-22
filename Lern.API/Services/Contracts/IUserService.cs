using Lern.API.Entities;
using Lern.API.Models;
using Lern.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Services.Contracts
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User GetById(int userId);
        User Create(User user, string password);
        IEnumerable<User> GetAll();
        bool Update(User user, string password = null);
        bool Delete(int id);
    }
}
