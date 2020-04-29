using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
