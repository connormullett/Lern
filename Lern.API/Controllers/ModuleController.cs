using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Lern.API.Entities;
using Lern.API.Helpers;
using Lern.API.Models;
using Lern.API.Models.Lesson;
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
    public class ModuleController : ControllerBase
    {
        private IModuleService _moduleService;
        private ILessonService _lessonService;
        private IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;

        public ModuleController(
            IModuleService moduleService,
            ILessonService lessonService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _lessonService = lessonService;
            _moduleService = moduleService;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        [HttpPost]
        public IActionResult Create([FromBody]ModuleCreateModel model)
        {
            var moduleModel = _mapper.Map<Module>(model);

            try
            {
                moduleModel.UserId = GetUserId();
                var module = _moduleService.Create(moduleModel);
                return Ok(_mapper.Map<ModuleModel>(module));
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
            var modules = _moduleService.GetAll();
            var model = _mapper.Map<IEnumerable<ModuleListModel>>(modules);
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var module = _moduleService.GetById(id);
            var model = _mapper.Map<ModuleModel>(module);

            var lessonQuery = _lessonService.GetLessonsByModuleId(id);
            var lessons = _mapper.Map<IEnumerable<LessonModel>>(lessonQuery).ToArray();
            model.Lessons = lessons;

            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("public/{userId}")]
        public IActionResult GetPublicByUserId(int userId)
        {
            var modules = _moduleService.GetPublicByUserId(userId);
            var model = _mapper.Map<IEnumerable<ModuleListModel>>(modules);
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("course/{courseId}")]
        public IActionResult GetModulesByCourseId(int courseId)
        {
            var modules = _moduleService.GetModulesByCourseId(courseId);
            var model = _mapper.Map<IEnumerable<ModuleListModel>>(modules);

            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ModuleUpdateModel model)
        {
            var moduleModel = _moduleService.GetById(id);

            if (moduleModel == null)
                return BadRequest(new { message = "Module not found" });

            if (GetUserId() != moduleModel.UserId)
                return Unauthorized();

            var module = _mapper.Map<Module>(model);
            module.UserId = moduleModel.UserId;

            if (model.IsPublic == null)
                module.IsPublic = moduleModel.IsPublic;
            else
                module.IsPublic = (bool)model.IsPublic;

            module.Id = id;

            try
            {
                _moduleService.Update(module);
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
                _moduleService.Delete(id);
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