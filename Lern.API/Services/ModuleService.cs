using Lern.API.Entities;
using Lern.API.Helpers;
using Lern.API.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Services
{
    public class ModuleService : IModuleService
    {

        private DataContext _context;
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;

        public ModuleService(DataContext context)
        {
            _context = context;
            _userService = new UserService();
            _courseService = new CourseService();
        }

        public ModuleService() { }

        public Module Create(Module model)
        {
            model.CreatedAt = DateTime.UtcNow;

            _context.Modules.Add(model);
            _context.SaveChanges();

            return model;
        }

        public bool Delete(int id)
        {
            var module = _context.Modules.Find(id);
            if (module != null)
                throw new AppException("Module not found");

            _context.Modules.Remove(module);
            return _context.SaveChanges() == 1;
        }

        public IEnumerable<Module> GetAll()
        {
            return _context.Modules;
        }

        public Module GetById(int id)
        {
            return _context.Modules.Find(id);
        }

        public IEnumerable<Module> GetModulesByCourseId(int courseId)
        {
            return _context.Modules.Where(x => x.CourseId == courseId);
        }

        public IEnumerable<Module> GetPublicByCourseId(int courseId)
        {
            return _context.Modules.Where(x => x.CourseId == courseId && x.IsPublic);
        }

        public IEnumerable<Module> GetPublicByUserId(int userId)
        {
            return _context.Modules.Where(x => x.IsPublic && x.UserId == userId);
        }

        public bool Update(Module moduleParam)
        {
            var module = _context.Modules.Find(moduleParam.Id);

            if (module == null)
                throw new AppException("Module not found");

            _context.Modules.Update(module);
            return _context.SaveChanges() == 1;
        }
    }
}
