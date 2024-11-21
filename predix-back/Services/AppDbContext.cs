using Microsoft.EntityFrameworkCore;
using predix_back.Models;

namespace predix_back.Services
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Prediction> Predictions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prediction>()
                .HasOne(p => p.User)           // Связь с моделью User
                .WithMany(u => u.Predictions)  // У пользователя может быть много Prediction
                .HasForeignKey(p => p.UserId); // Внешний ключ
        }
    }
}
