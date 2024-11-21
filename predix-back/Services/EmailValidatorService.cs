using System.Text.RegularExpressions;

namespace predix_back.Services
{
    public class EmailValidatorService
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            // Регулярное выражение для проверки email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase);
        }
    }
}
