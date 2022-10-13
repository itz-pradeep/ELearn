using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course,CourseDto>();
            CreateMap<CourseCreateDto,Course>();
            CreateMap<CourseUpdateDto,Course>();
        }
    }
}
