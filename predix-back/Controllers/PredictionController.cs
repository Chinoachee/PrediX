using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using predix_back.Contracts.Prediction;
using predix_back.Models;
using predix_back.Services;

namespace predix_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet("{userlogin}")]
        public async Task<IActionResult> GetByUserLogin(string userlogin)
        {
            var predictions = await _context.Predictions
                                .Where(p => p.User.Login == userlogin)
                                .Select(p => new
                                    {
                                        p.Id,
                                        p.Title,
                                        p.Description,
                                        p.CreatedAt
                                    })
                                .ToListAsync();

            if (!predictions.Any())
            {
                return NotFound("Пользователь или его предсказания не найдены");
            }

            return Ok(predictions);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrediction([FromBody] CreatePredictionRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == request.Login);
            if (user == null)
            {
                return BadRequest("Не удалось создать запись");
            }

            await _context.Predictions.AddAsync(new Prediction()
            {
                Title = request.PredictionTitle,
                Description = request.PredictionDescription,
                UserId = user.Id,
            });
            await _context.SaveChangesAsync();

            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
            
        //}
    }
}
