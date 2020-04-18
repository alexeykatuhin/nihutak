using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Data.Entities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(GetConnectionString());
         
        }

        private static string GetConnectionString()
        {
            const string databaseName = "webapijwt";
            const string databaseUser = "root";
            const string databasePass = "Head4372!";

            return $"Server=localhost;" +
                   $"database={databaseName};" +
                   $"uid={databaseUser};" +
                   $"pwd={databasePass};" +
                   $"pooling=true;";
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<IdentityRole>().HasData(
        //        new IdentityRole
        //        {
        //            //Id = "1",
        //            Name = "user"

        //        }
        //        );

        //}
    }
}
