namespace predix_back.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public long CreatedAt { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();
        public long LastEntry { get; set; } 
    }
}
