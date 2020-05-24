using Lern.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Services.Contracts
{
    public interface ILessonService
    {
        Lesson Create(Lesson model);
        Lesson GetById(int id);
        IEnumerable<Lesson> GetAll();
        bool Update(Lesson lessonParam);
        bool Delete(int id);
        IEnumerable<Lesson> GetLessonsByModuleId(int moduleId);
        IEnumerable<Lesson> GetAllPublicLessons();
        IEnumerable<Lesson> GetUsersLessons(int userId);
    }
}
