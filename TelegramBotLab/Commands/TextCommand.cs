namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Abstraction;
    using TelegramBotApi.Types;

    public class TextCommand : BaseCommand, ICommand
    {
        public TextCommand(ITelegramBot telegramBot) : base(telegramBot)
        {
        }

        public async Task Invoke(IRequest request)
        {
            await SendResponse(request.ChatId, MessageTemplate.Create().SetText("Your name is " + request.Text));
        }
    }
}
