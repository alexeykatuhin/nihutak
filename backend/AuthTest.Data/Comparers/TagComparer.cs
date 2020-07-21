using AuthTest.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AuthTest.Data.Comparers
{
    public class TagComparer : IEqualityComparer<PhotoTag>
    {
        public bool Equals([AllowNull] PhotoTag x, [AllowNull] PhotoTag y)
        {
            return x.PhotoId == y.PhotoId && x.TagId == y.TagId;
        }

        public int GetHashCode([DisallowNull] PhotoTag obj)
        {
            return obj.TagId.GetHashCode();
        }
    }
}
