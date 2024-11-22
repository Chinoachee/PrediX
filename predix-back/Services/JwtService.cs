using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace predix_back.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        private readonly string Key;
        private readonly string Issuer;
        private readonly string Audience;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            Key = _configuration["JWT:Key"] ?? throw new ArgumentNullException("JWT:Key", "Секретный ключ (JWT:Key) отсутствует в конфигурации.");
            Issuer = _configuration["JWT:Issuer"] ?? throw new ArgumentNullException("JWT:Issuer", "Издатель (JWT:Issuer) отсутствует в конфигурации.");
            Audience = _configuration["JWT:Audience"] ?? throw new ArgumentNullException("JWT:Audience", "Аудитория (JWT:Audience) отсутствует в конфигурации.");

            if (!_configuration.GetSection("JWT:AccessTokenExpiresInMinutes").Exists())
                throw new ArgumentNullException("JWT:AccessTokenExpiresInMinutes", "Срок действия Access Token отсутствует в конфигурации.");
        }

        public string GenerateAccessToken(string userId, string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            if (key.KeySize < 256)
            {
                throw new InvalidOperationException("Ключ должен быть не менее 256 бит.");
            }

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, username)
            };

            var tokenExpiration = TimeSpan.FromMinutes(15);
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(tokenExpiration),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
