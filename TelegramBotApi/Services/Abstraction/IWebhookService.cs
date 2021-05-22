namespace TelegramBotApi.Services.Abstraction
{
    using System.Threading.Tasks;
    using TelegramBotApi.Types;

    public interface IWebhookService
    {
        Task Process(Update update);
    }
}