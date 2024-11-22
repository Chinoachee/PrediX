namespace predix_back.Models
{
    public class Prediction
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Внешний ключ для пользователя
        public string Title { get; set; } // Название предсказания
        public string Description { get; set; } // Описание предсказания
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Дата создания

        // Навигационное свойство
        public User User { get; set; }
    }
}
