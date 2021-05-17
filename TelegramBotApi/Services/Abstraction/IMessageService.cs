namespace TelegramBotApi.Services.Abstraction
{
    using System.Threading.Tasks;
    using TelegramBotApi.Types;

    public interface IMessageService
    {
        Task HandleRequest(Message message);
    }
}
