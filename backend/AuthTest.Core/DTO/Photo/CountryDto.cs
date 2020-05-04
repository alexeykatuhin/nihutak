using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Core.DTO.Photo
{
    public class CountryDto: SimpleDto
    {
        public string Code { get; set; }
    }

    public class CoutryCitiesDto: CountryDto
    {
        public List<SimpleDto> Cities { get; set; }
    }
}
