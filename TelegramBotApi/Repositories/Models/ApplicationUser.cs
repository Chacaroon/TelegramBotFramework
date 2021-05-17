namespace TelegramBotApi.Repositories.Models
{
    public class ApplicationUser : Entity
    {

        public ApplicationUser(long chatId)
        {
            ChatId = chatId;
        }
        public long ChatId { get; set; }

        public int ChatStateId { get; set; }

        public ChatState ChatState { get; set; } = new ChatState();
    }
}