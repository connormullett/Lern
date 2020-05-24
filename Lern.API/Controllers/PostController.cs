using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Lern.API.Entities;
using Lern.API.Helpers;
using Lern.API.Models.Course;
using Lern.API.Models.Posts;
using Lern.API.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lern.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IOptions<AppSettings> _appSettings;

        public PostController(
            IMapper mapper,
            IPostService service,
            IOptions<AppSettings> settings)
        {
            _mapper = mapper;
            _postService = service;
            _appSettings = settings;
        }

        [HttpPost]
        public IActionResult Create([FromBody]PostCreateModel model)
        {
            var postModel = _mapper.Map<Post>(model);

            try
            {
                postModel.UserId = GetUserId();
                if (!_postService.Create(postModel))
                    return BadRequest("An internal error occured, try again");
                else
                    return Ok();
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var posts = _postService.GetAll();
            var model = _mapper.Map<IEnumerable<PostListItemModel>>(posts);
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var post = _postService.GetById(id);
            var model = _mapper.Map<PostModel>(post);
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("me")]
        public IActionResult GetMyPosts()
        {
            var userId = GetUserId();
            var posts = _postService.GetPostsByUserId(userId);
            var model = _mapper.Map<IEnumerable<PostListItemModel>>(posts);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]PostUpdateModel model)
        {
            var userId = GetUserId();
            var existingModel = _postService.GetById(id);

            if (userId != existingModel.UserId)
                return Unauthorized();

            var post = _mapper.Map<Post>(model);

            if (model.IsPublic == null)
                post.IsPublic = existingModel.IsPublic;
            else
                post.IsPublic = (bool)model.IsPublic;

            post.UserId = userId;
            post.PostId = id;

            try
            {
                _postService.Update(post);
                return Ok();
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = GetUserId();
            var existingModel = _postService.GetById(id);

            if (userId != existingModel.UserId)
                return Unauthorized();

            _postService.Delete(id);
            return Ok();
        }

        private int GetUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            return int.Parse(userId);
        }
    }
}