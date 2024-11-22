using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using predix_back.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(options =>
{
    string? issuer = builder.Configuration["Jwt:Issuer"];
    string? audience = builder.Configuration["Jwt:Audience"];
    string? key = builder.Configuration["JWT:Key"];
    if(issuer == null ||  audience == null || key == null)
    {
        Console.WriteLine("Не удалось найти настройки JWT токена");
        return;
    }
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

builder.Services.AddCors(options =>
{
    string? reactUrl = builder.Configuration["Cors"];
    if(reactUrl == null)
    {
        Console.WriteLine("Не удалось найти URL для React приложения, завершение работы");
        return;
    }
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(reactUrl);
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))   
);  // Настройка базы данных
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>(); // Регистрация сервиса хеширования паролей
builder.Services.AddScoped<IUserService, UserService>(); // Регистрация сервиса для работы с пользователями
builder.Services.AddAuthorization();    // Добавление авторизации

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();              // Включение CORS
app.UseAuthentication();    // Аутентификация
app.UseAuthorization();     // Авторизация

app.MapControllers();

app.Run();