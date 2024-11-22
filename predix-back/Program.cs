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
        Console.WriteLine("�� ������� ����� ��������� JWT ������");
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
        Console.WriteLine("�� ������� ����� URL ��� React ����������, ���������� ������");
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
);  // ��������� ���� ������
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>(); // ����������� ������� ����������� �������
builder.Services.AddScoped<IUserService, UserService>(); // ����������� ������� ��� ������ � ��������������
builder.Services.AddAuthorization();    // ���������� �����������

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();              // ��������� CORS
app.UseAuthentication();    // ��������������
app.UseAuthorization();     // �����������

app.MapControllers();

app.Run();