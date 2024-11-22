using System.ComponentModel.DataAnnotations;

namespace predix_back.Contracts.User
{
    public record AuthorizateRequest(
    [Required(ErrorMessage = "Логин обязателен.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин должен содержать от 3 до 50 символов.")]
    string Login,

    [Required(ErrorMessage = "Пароль обязателен.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 20 символов.")]
    string Password
    );
}
