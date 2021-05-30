namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.ChatState;
    using TelegramBotApi.Types;

    public class StartCommand : CommandBase
    {
        public async Task Invoke()
        {
            await TelegramBot.SetChatStateAsync(new ChatState
            {
                WaitingFor = GetCommandName<NameCommand>()
            });

            await SendMessageAsync(
                MessageTemplate.Create()
                    .SetText("Hello! What's your name?"));
        }
    }
}
