﻿using Lern.API.Entities;
using Lern.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lern.API.Models.Lesson;
using Lern.API.Models;
using Lern.API.Models.Module;
using Lern.API.Models.Course;

namespace Lern.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            CreateMap<User, AuthenticateModel>();

            CreateMap<Lesson, LessonModel>();
            CreateMap<LessonListModel, Lesson>();
            CreateMap<LessonCreateModel, Lesson>();
            CreateMap<LessonUpdateModel, Lesson>();
            CreateMap<LessonModel, Lesson>();

            CreateMap<ModuleCreateModel, Module>();
            CreateMap<ModuleListModel, Module>();
            CreateMap<ModuleUpdateModel, Module>();
            CreateMap<ModuleModel, Module>();
            CreateMap<Module, ModuleModel>();

            CreateMap<CourseCreateModel, Course>();
            CreateMap<Course, CourseListItemModel>();
            CreateMap<CourseUpdateModel, Course>();
            CreateMap<CourseModel, Course>();
            CreateMap<Course, CourseModel>();
        }
    }
}
