using Microsoft.AspNetCore.Mvc;
using predix_back.Contracts.User;
using predix_back.Services;
using predix_back.Models;
using Microsoft.EntityFrameworkCore;

namespace predix_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet("{login}")]
        public IActionResult GetByLogin(string login)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == login);
            if (user == null)
            {
                return NotFound();
            }
            var response = new GetByLoginResponse(user.Login, DateTimeOffset.FromUnixTimeSeconds(user.LastEntry).LocalDateTime);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromBody] RegisterUserRequest request)
        {
            if (request == null)
            {
                return BadRequest("Тело запроса пусто");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool userExists = await _context.Users.AnyAsync(u => u.Login == request.Login || u.Email == request.Email);

            if (userExists)
            {
                return BadRequest("Пользователь с таким логином или email уже существует");
            }

            await _context.Users.AddAsync(new User()
            {
                Login = request.Login,
                Email = request.Email,
                Password = HashPasswordService.HashPassword(request.Password),
                LastEntry = DateTimeOffset.Now.ToUnixTimeSeconds()
            });
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("authorization")]
        public async Task<IActionResult> Authorization([FromBody] AuthorizationUserRequest request)
        {
            if (request == null)
            {
                return BadRequest("Тело запроса пусто");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.Login);
            if (user == null)
            {
                return BadRequest("Неправильный логин или пароль");
            }
            if (!HashPasswordService.VerifyPassword(request.Password, user.Password))
            {
                return BadRequest("Неправильный логин или пароль");
            }

            // Добавить создание JWT токена и отдать его в Ok();
            return Ok();
        }
    }
}
