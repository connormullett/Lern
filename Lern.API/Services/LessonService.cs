using Lern.API.Entities;
using Lern.API.Helpers;
using Lern.API.Models.Lesson;
using Lern.API.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Services
{
    public class LessonService : ILessonService
    {
        private DataContext _context;
        private readonly IUserService _userService;
        private readonly IModuleService _moduleService;

        public LessonService(DataContext context)
        {
            _context = context;
            _userService = new UserService();
            _moduleService = new ModuleService();
        }

        public LessonService() { }

        public Lesson Create(Lesson model)
        {
            model.CreatedAt = DateTime.UtcNow;

            _context.Lessons.Add(model);
            _context.SaveChanges();

            return model;
        }

        public bool Delete(int id)
        {
            var lesson = _context.Lessons.Find(id);
            if (lesson != null)
                throw new AppException("Lesson not found");

            _context.Lessons.Remove(lesson);
            return _context.SaveChanges() == 1;
        }

        public IEnumerable<Lesson> GetAll()
        {
            return _context.Lessons;
        }

        public Lesson GetById(int id)
        {
            var lesson = _context.Lessons.Find(id);
            if (lesson == null)
                throw new AppException("Lesson not found");

            return lesson;
        }

        public bool Update(Lesson lessonParam)
        {
            var lesson = _context.Lessons.Find(lessonParam.Id);

            if (lesson == null)
                throw new AppException("Lesson not found");

            _context.Lessons.Update(lesson);
            return _context.SaveChanges() == 1;
        }

        public IEnumerable<Lesson> GetPublicByUserId(int userId)
        {
            return _context.Lessons.Where(x => x.IsPublic && x.UserId == userId);
        }

        public IEnumerable<Lesson> GetLessonsByModuleId(int moduleId)
        {
            return _context.Lessons.Where(x => x.ModuleId == moduleId);
        }

        public IEnumerable<Lesson> GetPublicByModuleId(int moduleId)
        {
            return _context.Lessons.Where(x => x.ModuleId == moduleId && x.IsPublic);
        }
    }
}
