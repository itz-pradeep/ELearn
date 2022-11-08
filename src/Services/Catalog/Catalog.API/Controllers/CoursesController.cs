using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [Route("api/v1.0/lms/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CoursesController> _logger;
        private readonly IMapper _mapper;

        public CoursesController(ICourseRepository courseRepository
            ,ILogger<CoursesController> logger
            ,IMapper mapper)
        {
            _courseRepository = courseRepository;
            _logger = logger;
            _mapper = mapper;
        }
        // GET: api/v1/<CatalogController>
        [HttpGet("getall")]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), (int)HttpStatusCode.OK)]
        [Authorize(Roles = "ADMIN,NORMAL_USER")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _courseRepository.GetCoursesAsync();
            var coursesToReturn = _mapper.Map<IEnumerable<CourseDto>>(courses);

            _logger.LogInformation($"Results found = {courses.Count()}");
             
            return Ok(coursesToReturn);

        }

        // GET api/<CatalogController>/shfdjshfidshfkdsh
        [HttpGet("{id}",Name = "GetCourse")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CourseDto), (int)HttpStatusCode.OK)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<CourseDto>> GetCourseById(string id)
        {
            var course = await _courseRepository.GetCourseAsync(id);
            if(course == null)
            {
                _logger.LogError($"Course with ID {id} not found.");
                return NotFound();
            }

            var courseToReturn = _mapper.Map<CourseDto>(course);

            return Ok(courseToReturn);

        }

        [HttpPost("info", Name = "GetByFilter")]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), (int)HttpStatusCode.OK)]
        [Authorize(Roles = "ADMIN,NORMAL_USER")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetByFilter(FilterDto filter)
        {
            var courses = await _courseRepository.GetCourseByFilterAsync(filter);
      
            var coursesToReturn = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return Ok(coursesToReturn);

        }

        // POST api/<CatalogController>
        [HttpPost]
        [ProducesResponseType(typeof(Course),(int)HttpStatusCode.OK)]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] CourseCreateDto request)
        {
            var course = _mapper.Map<Course>(request);
            course.CreatedDate = DateTimeOffset.Now;
            course.IsActive = true;

            await _courseRepository.CreateCourseAsync(course);
            return CreatedAtRoute("GetCourse",new { Id  = course.Id}, course);
        }

        // PUT api/<CatalogController>/5
        [HttpPut()]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseUpdateDto request)
        {
            var course = _mapper.Map<Course>(request);

            return Ok(await _courseRepository.UpdateCourseAsync(course));
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}",Name = "DeleteCourse")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _courseRepository.DeleteCourseAsync(id));
        }
    }
}
