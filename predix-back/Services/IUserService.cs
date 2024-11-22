using predix_back.Models;

namespace predix_back.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailAsync(string email); // Проверка по email
        Task<User?> GetUserByLoginAsync(string login); // Проверка по логину
        Task<bool> CreateUserAsync(User user); // Создание нового пользователя
        Task<User?> GetUserByIdAsync(int id); // Поиск пользователя по Id
        Task UpdateLastEntryAsync(int id, DateTime lastEntry); // Обновление последнего действия пользователя
    }
}
