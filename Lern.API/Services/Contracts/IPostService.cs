using Lern.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Services.Contracts
{
    public interface IPostService
    {
        bool Create(Post model);
        Post GetById(int id);
        bool Update(Post postParam);
        bool Delete(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetPostsByUserId(int userId);
    }
}
