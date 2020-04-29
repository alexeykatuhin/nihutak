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


    }
}
