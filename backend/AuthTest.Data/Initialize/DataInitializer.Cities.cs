using AuthTest.Data.Entities;
using AuthTest.Data.Extensions;
using AuthTest.Enums;
using Microsoft.EntityFrameworkCore;

namespace AuthTest.Data.Initialize
{
    public static partial class DataInitializer
    {
        public static void SeedCities(ApplicationDbContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
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
    },
    new City
    {
        Id = (int)Enums.CityEnum.Almaty,
        Name = "Алматы",
        CountryId = (int)CountryEnum.Kz
    },
    new City
    {
        Id = (int)Enums.CityEnum.Crimea,
        Name = "Крым",
        CountryId = (int)CountryEnum.Russia
    });
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Cities ON;");

                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Cities OFF");
                transaction.Commit();
            }


        }
    }
}
