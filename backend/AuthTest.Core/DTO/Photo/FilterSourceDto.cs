using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Core.DTO.Photo
{
    public class FilterSourceDto
    {
        public List<SimpleDto> Tags { get; set; }
        public List<CoutryCitiesDto> Countries { get; set; }
    }
}
