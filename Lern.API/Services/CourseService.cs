using Lern.API.Entities;
using Lern.API.Helpers;
using Lern.API.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Services
{
    public class CourseService : ICourseService
    {
        private DataContext _context;
        private readonly IUserService _userService;

        public CourseService(DataContext context)
        {
            _context = context;
            _userService = new UserService();
        }

        public CourseService() { }

        public Course Create(Course model)
        {
            model.CreatedAt = DateTime.UtcNow;

            _context.Courses.Add(model);
            _context.SaveChanges();

            return model;
        }

        public bool Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
                throw new AppException("Course not found");

            _context.Courses.Remove(course);
            return _context.SaveChanges() == 1;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.Where(x => x.IsPublic);
        }

        public Course GetById(int id)
        {
            return _context.Courses.Find(id);
        }

        public IEnumerable<Course> GetPublicByUserId(int userId)
        {
            return _context.Courses.Where(x => x.IsPublic && x.UserId == userId);
        }

        public bool Update(Course courseParam)
        {
            var course = _context.Courses.Find(courseParam.Id);

            if (course == null)
                throw new AppException("Course not found");

            if (!string.IsNullOrEmpty(courseParam.Description))
                course.Description = courseParam.Description;

            if(!string.IsNullOrEmpty(courseParam.Title))
                course.Title = courseParam.Title;

            course.IsPublic = courseParam.IsPublic;
            course.ModifiedAt = DateTime.UtcNow;

            _context.Courses.Update(course);
            return _context.SaveChanges() == 1;
        }

        public bool CourseTitleIsTaken(string title)
        {
            if (_context.Courses.Where(x => x.Title == title) == null)
                return true;
            return false;
        }

        public object GetByUserId(int userId)
        {
            return _context.Courses.Where(x => x.UserId == userId);
        }
    }
}
