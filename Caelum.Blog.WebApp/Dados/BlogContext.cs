using Microsoft.EntityFrameworkCore;
using Caelum.Blog.WebApp.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Caelum.Blog.WebApp.Dados
{
    public class BlogContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name
                        && level == LogLevel.Information)
                    .AddConsole();
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration cfg = builder.Build();
            var stringCnx = cfg.GetConnectionString("Blog");

            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(stringCnx);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Autor)
                .WithMany()
                .HasForeignKey(p => p.IdAutor);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
