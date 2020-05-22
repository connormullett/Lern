using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Lern.API.Entities;
using Lern.API.Helpers;
using Lern.API.Models.Lesson;
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
    public class LessonController : ControllerBase
    {
        private ILessonService _lessonService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public LessonController(
            ILessonService lessonService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _lessonService = lessonService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public IActionResult Create([FromBody]LessonCreateModel model)
        {
            var lessonModel = _mapper.Map<Lesson>(model);

            try
            {
                lessonModel.UserId = GetUserId();
                var lesson = _lessonService.Create(lessonModel);
                return Ok(_mapper.Map<LessonModel>(lesson));
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
            var lessons = _lessonService.GetAll();
            var model = _mapper.Map<IEnumerable<LessonListModel>>(lessons);
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var lesson = _lessonService.GetById(id);
                var model = _mapper.Map<LessonModel>(lesson);
                return Ok(model);
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("public")]
        [HttpGet("{userId}")]
        public IActionResult GetPublicByUserId(int userId)
        {
            var lessons = _lessonService.GetPublicByUserId(userId);
            var model = _mapper.Map<IEnumerable<LessonListModel>>(lessons);
            return Ok(model);
        }

        [AllowAnonymous]
        [Route("module")]
        [HttpGet("{moduleId}")]
        public IActionResult GetLessonsByModuleId(int moduleId)
        {
            var lessons = _lessonService.GetLessonsByModuleId(moduleId);
            var model = _mapper.Map<IEnumerable<LessonListModel>>(lessons);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]LessonUpdateModel model)
        {
            if (GetUserId() != id)
                return Unauthorized();

            var lesson = _mapper.Map<Lesson>(model);
            lesson.UserId = id;

            try
            {
                _lessonService.Update(lesson);
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
            if (GetUserId() != id)
                return Unauthorized();

            try
            {
                _lessonService.Delete(id);
                return Ok();
            }
            catch(AppException ex)
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