namespace TelegramBotApi.Models.Abstraction
{
    public interface IRequest
    {
        long ChatId { get; set; }

        string Text { get; set; }
    }
}
