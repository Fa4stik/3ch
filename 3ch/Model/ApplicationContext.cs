using Microsoft.EntityFrameworkCore;
using System;

namespace _3ch.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Post> Post { get; set; } = null!;
        public DbSet<Comment> Comment { get; set; } = null!;
        public DbSet<Tag> Tag { get; set; } = null!;
        public DbSet<Media> Media { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("connection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
