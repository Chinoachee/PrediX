using Microsoft.EntityFrameworkCore;
using predix_back.Models;

namespace predix_back.Services
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связи между User и Prediction
            modelBuilder.Entity<Prediction>()
                .HasOne(p => p.User)           // Один User
                .WithMany(u => u.Predictions)  // Может иметь много Prediction
                .HasForeignKey(p => p.UserId)  // Внешний ключ
                .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление

            // Настройка связи между User и RefreshToken
            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)          // Один User
                .WithMany(u => u.RefreshTokens) // Может иметь много RefreshToken
                .HasForeignKey(rt => rt.UserId) // Внешний ключ
                .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление

            // Ограничение длины текста в Prediction
            modelBuilder.Entity<Prediction>()
                .Property(p => p.Title)
                .HasMaxLength(100);

            modelBuilder.Entity<Prediction>()
                .Property(p => p.Description)
                .HasMaxLength(1000);
        }
    }
}
