using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Data.Entities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration) : base()
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var s = _configuration["ConnStr"];


            optionsBuilder.UseSqlServer(s);
         
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PhotoTag>().HasKey(sc => new { sc.PhotoId, sc.TagId });

            builder.Entity<PhotoTag>().HasOne(p => p.Photo).WithMany(p => p.PhotoTags).HasForeignKey(x => x.PhotoId);
            builder.Entity<PhotoTag>().HasOne(p => p.Tag).WithMany(p => p.PhotoTags).HasForeignKey(x => x.TagId);
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PhotoTag> PhotoTags { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }



        private static string GetConnectionString()
        {
            //const string databaseName = "webapijwt";
            //const string databaseUser = "root";
            //const string databasePass = "Head4372!";
            //const string server = "localhost";

            const string databaseName = "u1043325_nihutak";
            const string databaseUser = "u1043_nihutak";
            const string databasePass = "Head4372!";
            const string server = "localhost:3306";

            return $"Server={server};" +
                   $"database={databaseName};" +
                   $"uid={databaseUser};" +
                   $"pwd={databasePass};" +
                   $"pooling=true;";
        }


    }
}
