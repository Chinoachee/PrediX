using System.ComponentModel.DataAnnotations;

namespace predix_back.Contracts.User
{
    public record RegisterUserRequest(
        [Required(ErrorMessage = "Логин обязателен")] 
        string Login,

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        string Email,

        [Required(ErrorMessage = "Пароль обязателен")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 20 символов")]
        string Password
    );
}
