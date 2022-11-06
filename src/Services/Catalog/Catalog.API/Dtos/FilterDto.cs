using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Dtos
{
    public class FilterDto
    {
        public string Technology { get; set; }
        public int DurationFromRange { get; set; }
        public int DurationToRange { get; set; }
    }
}
