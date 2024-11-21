using System.ComponentModel.DataAnnotations;

namespace predix_back.Contracts.User
{
    public record AuthorizationUserRequest(
        [Required(ErrorMessage = "Логин обязателен")]
        string Login,
        [Required(ErrorMessage = "Пароль обязателен")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 20 символов")]
        string Password
    );
}
