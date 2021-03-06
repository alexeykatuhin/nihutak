﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Data.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
