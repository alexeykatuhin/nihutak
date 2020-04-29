using AuthTest.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuthTest.Data.Entities
{
    public class Photo
    {
        public int Id{ get; set; }
        [MaxLength(300)]
        public string Url { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public CountryEnum CountryId { get; set; }
        public CityEnum CityId { get; set; }
        public IList<PhotoTag> PhotoTags { get; set; }
        public DateTime CreatedDt { get; set; }

    }
}
