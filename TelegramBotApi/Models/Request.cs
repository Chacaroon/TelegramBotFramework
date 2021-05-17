namespace TelegramBotApi.Models
{
    using TelegramBotApi.Models.Abstraction;

    public class Request : IRequest
    {
        public long ChatId { get; set; }

        public string Text { get; set; }

        public Request(long chatId,
            string text)
        {
            ChatId = chatId;
            Text = text;
        }
    }
}
