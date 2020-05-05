using AuthTest.Data.Entities;
using AuthTest.Data.Extensions;
using AuthTest.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Data.Initialize
{
    public static partial class DataInitializer
    {
        public static void SeedCountries(ApplicationDbContext context)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                context.Countries.AddOrUpdate(new Country
                {
                    Id = (int)CountryEnum.Russia,
                    Code = "ru",
                    Name = "Россия"

                },
             new Country
             {
                 Id = (int)CountryEnum.Kz,
                 Code = "kz",
                 Name = "Казахстан"

             });
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Countries ON;");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Countries OFF");
                transaction.Commit();
            }
        }
    }
}
