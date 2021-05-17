namespace TelegramBotApi.Repositories.Models
{
    using System;

    public class Entity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public Entity()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
