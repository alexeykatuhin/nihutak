using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Core.DTO.Photo
{
    public class PhotoFilterDto
    {
        public int Page { get; set; }
        public List<int> Tags { get; set; }
        public List<int> Countries { get; set; }
        public List<int> Cities { get; set; }
        public List<int> Years { get; set; }
    }
}
