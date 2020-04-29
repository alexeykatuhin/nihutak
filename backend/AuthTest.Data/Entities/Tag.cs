using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<PhotoTag> PhotoTags { get; set; }
    }

    
}
