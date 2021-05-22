namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;

    public class ErrorCommand : CommandBase
    {
        public async Task Invoke(Request request)
        {
            await SendResponse(request.ChatId, MessageTemplate.Create().SetText("Something went wrong"));
        }
    }
}
