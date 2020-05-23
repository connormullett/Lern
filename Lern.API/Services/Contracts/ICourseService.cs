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
        IEnumerable<Course> GetAll();
        bool Update(Course courseParam);
        bool Delete(int id);
        IEnumerable<Course> GetPublicByUserId(int userId);
        bool CourseTitleIsTaken(string title);
        object GetByUserId(int userId);
    }
}
