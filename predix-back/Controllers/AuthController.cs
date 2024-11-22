using Microsoft.AspNetCore.Mvc;
using predix_back.Contracts.User;
using predix_back.Models;
using predix_back.Services;

namespace predix_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(JwtService jwtService, IUserService userService, IPasswordHasher passwordHasher) : ControllerBase
    {
        private readonly JwtService _jwtService = jwtService;

        private readonly IUserService _userService = userService;

        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emailTask = _userService.GetUserByEmailAsync(request.Email);
            var loginTask = _userService.GetUserByLoginAsync(request.Login);

            await Task.WhenAll(emailTask, loginTask);

            if (emailTask.Result != null)
            {
                return Conflict(new { message = "Пользователь с такой почтой уже существует." });
            }
            if (loginTask.Result != null)
            {
                return Conflict(new { message = "Пользователь с таким логином уже существует." });
            }

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var newUser = new User
            {
                Login = request.Login,
                Email = request.Email,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                IsActive = false
            };

            var result = await _userService.CreateUserAsync(newUser);
            if (!result)
            {
                return StatusCode(500, new { message = "Ошибка при регистрации. Пожалуйста попробуйте позже." });
            }

            return Ok(new { message = "Успешная регистрация." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthorizateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUserByLoginAsync(request.Login);
            if (user == null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
            {
                return Unauthorized(new { message = "Неправильный логин или пароль." });
            }

            var accessToken = _jwtService.GenerateAccessToken(user.Id.ToString(), user.Login);
            var refreshToken = _jwtService.GenerateRefreshToken();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

    }
}