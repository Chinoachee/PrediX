using predix_back.Services;
using System.Security.Claims;

namespace predix_back.Middlewares
{
    public class UpdateLastEntryMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userId, out var parsedUserId))
                {
                    await userService.UpdateLastEntryAsync(parsedUserId, DateTime.UtcNow);
                }
            }

            await _next(context);
        }
    }

}
