namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;

    public class StartCommand : CommandBase
    {
        public async Task Invoke(MessageRequest request)
        {
            await SendResponse(
                request.ChatId,
                MessageTemplate.Create()
                    .SetText("Hello!"));
        }
    }
}
