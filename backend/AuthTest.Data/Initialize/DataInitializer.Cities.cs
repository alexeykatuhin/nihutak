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
        public static void SeedCities(ApplicationDbContext context)
        {
            context.Cities.AddOrUpdate(new City
            {
                Id = (int)Enums.CityEnum.Zvenigorod,
                Name = "Звенигород",
                CountryId = (int)CountryEnum.Russia
            },
            new City
            {
                Id = (int)Enums.CityEnum.Moscow,
                Name = "Москва",
                CountryId = (int)CountryEnum.Russia
            },
            new City
            {
                Id = (int)Enums.CityEnum.Mozhaysk,
                Name = "Можайск",
                CountryId = (int)CountryEnum.Russia
            });

            context.SaveChanges();
        }
    }
}
