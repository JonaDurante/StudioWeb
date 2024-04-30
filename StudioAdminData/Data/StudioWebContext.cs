using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioData.Models.Business;

namespace StudioData.Data
{
    public class StudioWebContext : IdentityDbContext<StudioWebUser>
    {
        private readonly ILoggerFactory _loggerFactory;
        public StudioWebContext(DbContextOptions<StudioWebContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AvailableDay>()
                .HasIndex(c => c.Date) // Crear un índice en la columna Date
                .IsUnique(); // Especificar que debe ser único
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<AvailableDay> AvailableDays { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Third> Thirds { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger = _loggerFactory.CreateLogger<StudioWebContext>();
            optionsBuilder.LogTo(d => logger.Log(LogLevel.Error, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Error)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }


    }
}
