using AuthTest.Data.Entities;
using AuthTest.Data.Extensions;
using AuthTest.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Data.Initialize
{
    public static partial class DataInitializer
    {
        public static void SeedCountries(ApplicationDbContext context)
        {
            context.Countries.AddOrUpdate(new Country
            {
                Id = (int)CountryEnum.Russia,
                Code = "ru",
                Name = "Россия"

            });
            context.SaveChanges();
        }
    }
}
