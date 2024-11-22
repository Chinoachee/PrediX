using Microsoft.EntityFrameworkCore;
using predix_back.Models;

namespace predix_back.Services
{
    public class UserService(AppDbContext context) : IUserService
    {
        private readonly AppDbContext _context = context;
        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task UpdateLastEntryAsync(int id, DateTime lastEntry)
        {
            var user = new User { Id = id, LastEntry = lastEntry };
            _context.Users.Attach(user);
            _context.Entry(user).Property(u => u.LastEntry).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }
}
