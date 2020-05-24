using Lern.API.Entities;
using Lern.API.Helpers;
using Lern.API.Models.Posts;
using Lern.API.Services.Contracts;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lern.API.Services
{
    public class PostService : IPostService
    {
        private DataContext _context;

        public PostService(DataContext context)
        {
            _context = context;
        }

        public bool Create(Post model)
        {
            if (TitleIsTaken(model.Title))
                throw new AppException("Title already exists");

            model.CreatedAt = DateTime.UtcNow;

            _context.Posts.Add(model);
            return _context.SaveChanges() == 1;
        }

        public bool Delete(int id)
        {
            var entity = _context.Posts.Find(id);

            _context.Posts.Remove(entity);
            return _context.SaveChanges() == 1;
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts;
        }

        public Post GetById(int id)
        {
            return _context.Posts.Find(id);
        }

        public IEnumerable<Post> GetPostsByUserId(int userId)
        {
            return _context.Posts.Where(x => x.UserId == userId);
        }

        public bool Update(Post postParam)
        {
            var post = _context.Posts.Find(postParam.PostId);

            if (post == null)
                throw new AppException("Post not found");

            if (!string.IsNullOrEmpty(postParam.Title))
                post.Title = postParam.Title;

            if (!string.IsNullOrEmpty(postParam.Body))
                post.Body = postParam.Body;

            post.IsPublic = postParam.IsPublic;
            post.ModifiedAt = DateTime.UtcNow;

            _context.Posts.Update(post);
            return _context.SaveChanges() == 1;
        }

        private bool TitleIsTaken(string title)
        {
            var obj = _context.Posts.Where(x => x.Title == title).FirstOrDefault();
            if (obj == null) return false;
            return true;
        }
    }
}
