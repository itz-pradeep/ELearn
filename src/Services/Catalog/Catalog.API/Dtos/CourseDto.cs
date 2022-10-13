using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Dtos
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public string Technology { get; set; }
        public string LaunchUrl { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
