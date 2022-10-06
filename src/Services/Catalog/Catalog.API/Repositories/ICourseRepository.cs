using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseAsync(string id);
        Task<IEnumerable<Course>> GetCourseByTechnologyAsync(string Technology);
        Task CreateCourseAsync(Course entity);
        Task<bool> UpdateCourseAsync(Course entity);
        Task<bool> DeleteCourseAsync(string id);
    }
}
