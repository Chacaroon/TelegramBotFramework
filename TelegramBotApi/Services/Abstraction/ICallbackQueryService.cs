namespace TelegramBotApi.Services.Abstraction
{
    using System.Threading.Tasks;
    using TelegramBotApi.Types.ReplyMarkup;

    public interface ICallbackQueryService
    {
        Task HandleRequest(CallbackQuery callbackQuery);
    }
}
