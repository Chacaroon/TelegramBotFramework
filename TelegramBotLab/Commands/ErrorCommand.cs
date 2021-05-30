namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Types;

    public class ErrorCommand : CommandBase
    {
        public async Task Invoke()
        {
            await SendMessageAsync(MessageTemplate.Create()
                .SetText("Something went wrong =("));
        }
    }
}
