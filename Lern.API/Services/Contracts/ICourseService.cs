using Lern.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Services.Contracts
{
    public interface ICourseService
    {
        Course Create(Course model);
        Course GetById(int id);
        IEnumerable<Course> GetAllPublicCourses();
        IEnumerable<Course> GetUserCourses(int userId);
        bool Update(Course courseParam);
        bool Delete(int id);
        bool CourseTitleIsTaken(string title);
        IEnumerable<Course> GetByUserId(int userId);
        IEnumerable<Course> GetAll();
    }
}
