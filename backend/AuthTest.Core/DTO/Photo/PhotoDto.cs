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
        public CountryEnum CountryId { get; set; }
        public CityEnum CityId { get; set; }
        public List<TagDto> Tags { get; set; }
        public string Date { get; set; }
    }

    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
