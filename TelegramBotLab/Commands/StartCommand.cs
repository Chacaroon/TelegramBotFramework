namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.ChatState;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;

    public class StartCommand : CommandBase
    {
        public async Task Invoke(MessageRequest request)
        {
            await TelegramBot.SetChatStateAsync(new ChatState
            {
                WaitingFor = "some"
            });

            await SendResponseAsync(
                MessageTemplate.Create()
                    .SetText("Hello!"));
        }
    }
}
