using AuthTest.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Core.DTO.Photo
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }        
        public List<SimpleDto> Tags { get; set; }
        public string Date { get; set; }
        public SimpleDto City { get; set; }
        public CountryDto Country { get; set; }
    }

}
