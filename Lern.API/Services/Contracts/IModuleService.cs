using Lern.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Services.Contracts
{
    public interface IModuleService
    {
        Module Create(Module model);
        Module GetById(int id);
        IEnumerable<Module> GetAll();
        bool Update(Module moduleParam);
        bool Delete(int id);
        IEnumerable<Module> GetModulesByCourseId(int courseId);
        IEnumerable<Module> GetAllPublicModules();
        IEnumerable<Module> GetUsersModules(int userId);
    }
}
