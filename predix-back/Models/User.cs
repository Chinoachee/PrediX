using System.ComponentModel.DataAnnotations;

namespace predix_back.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Логин обязателен.")]
        [MinLength(3, ErrorMessage = "Логин должен содержать не менее 3 символов.")]
        [MaxLength(50, ErrorMessage = "Логин не должен превышать 50 символов.")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email обязателен.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email должен быть от 5 до 100 символов.")]
        [EmailAddress(ErrorMessage = "Введите корректный email-адрес.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 20 символов")]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? LastEntry { get; set; }

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public bool IsActive { get; set; }

        public ICollection<Prediction> Predictions { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
