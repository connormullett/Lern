using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Lern.API.Entities;
using Lern.API.Helpers;
using Lern.API.Models.Course;
using Lern.API.Models.Module;
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
    public class CourseController : ControllerBase
    {
        private ICourseService _courseService;
        private IModuleService _moduleService;
        private IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;

        public CourseController(
            ICourseService courseService,
            IModuleService moduleService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _courseService = courseService;
            _moduleService = moduleService;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CourseCreateModel model)
        {
            if (_courseService.CourseTitleIsTaken(model.Title))
                return BadRequest(new { message = "Title already taken" });

            var courseModel = _mapper.Map<Course>(model);

            try
            {
                courseModel.UserId = GetUserId();
                var course = _courseService.Create(courseModel);
                return Ok(_mapper.Map<CourseModel>(course));
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
            var courses = _courseService.GetAll();
            var model = _mapper.Map<IEnumerable<CourseListItemModel>>(courses);
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _courseService.GetById(id);
            var model = _mapper.Map<CourseModel>(course);

            var moduleQuery = _moduleService.GetModulesByCourseId(model.Id);
            var modules = _mapper.Map<IEnumerable<ModuleModel>>(moduleQuery).ToArray();
            model.Modules = modules;

            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("public/{userId}")]
        public IActionResult GetPublicByUserId(int userId)
        {
            var courses = _courseService.GetPublicByUserId(userId);
            var model = _mapper.Map<IEnumerable<CourseListItemModel>>(courses);
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("user/{userId}")]
        public IActionResult GetCourseByUserId(int userId)
        {
            var courses = _courseService.GetByUserId(userId);
            var model = _mapper.Map<IEnumerable<CourseListItemModel>>(courses);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]CourseUpdateModel model)
        {
            var userId = GetUserId();

            if (userId != _courseService.GetById(id).UserId)
                return Unauthorized();

            var course = _mapper.Map<Course>(model);

            if (model.IsPublic == null)
                course.IsPublic = _courseService.GetById(id).IsPublic;
            else
                course.IsPublic = (bool)model.IsPublic;

            course.UserId = userId;
            course.Id = id;

            try
            {
                _courseService.Update(course);
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
            var course = _courseService.GetById(id);

            if (GetUserId() != course.UserId)
                return Unauthorized();

            try
            {
                _courseService.Delete(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private int GetUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            return int.Parse(userId);
        }
    }
}