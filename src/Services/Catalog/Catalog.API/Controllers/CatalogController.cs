using Catalog.API.Entities;
using Catalog.API.Repositories;
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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ICourseRepository courseRepository,ILogger<CatalogController> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }
        // GET: api/v1/<CatalogController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Course>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _courseRepository.GetCoursesAsync();
            return Ok(courses);

        }

        // GET api/<CatalogController>/shfdjshfidshfkdsh
        [HttpGet("{id}",Name = "GetCourse")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Course>> GetCourseById(string id)
        {
            var course = await _courseRepository.GetCourseAsync(id);
            if(course == null)
            {
                _logger.LogError($"Course with ID {id} not found.");
                return NotFound();
            }

            return Ok(course);

        }

        // POST api/<CatalogController>
        [HttpPost]
        [ProducesResponseType(typeof(Course),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] Course course)
        {
            await _courseRepository.CreateCourseAsync(course);
            return CreatedAtRoute("GetCourse",new { Id  = course.Id}, course);
        }

        // PUT api/<CatalogController>/5
        [HttpPut()]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCourse([FromBody] Course course)
        {
            return Ok(await _courseRepository.UpdateCourseAsync(course));
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}",Name = "DeleteCourse")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _courseRepository.DeleteCourseAsync(id));
        }
    }
}
