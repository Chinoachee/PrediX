namespace predix_back.Models
{
    public class Prediction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CreatedAt { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();
    }
}
