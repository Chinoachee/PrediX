using Microsoft.EntityFrameworkCore;
using predix_back.Models;

namespace predix_back.Services
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
