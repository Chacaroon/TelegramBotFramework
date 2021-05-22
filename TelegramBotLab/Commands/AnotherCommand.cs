namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;

    public class AnotherCommand : CommandBase
    {
        public async Task Invoke(QueryRequest request)
        {
            await SendResponse(
                request.ChatId,
                request.MessageId,
                MessageTemplate.Create().SetText("Enter your name:"));
        }
    }
}
