namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;

    public class NameCommand : CommandBase
    {
        public async Task Invoke(MessageRequest request)
        {
            var message = MessageTemplate.Create()
                .SetText($"Nice to meet you, {request.Text}! Let's start studying");

            await SendMessageAsync(message);

            await Redirect(GetCommandName<MenuCommand>());
        }
    }
}
