namespace TelegramBotApi.Services.Abstraction
{
    using TelegramBotApi.Types;

    public interface IWebhookService
    {
        void Process(Update update);
    }
}