namespace TelegramBotLab.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TelegramBotApi;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Abstraction;
    using TelegramBotApi.Types;

    public class UndefinedCommand : BaseCommand, ICommand
    {
        public UndefinedCommand(ITelegramBot telegramBot) : base(telegramBot)
        {
        }

        public async Task Invoke(IRequest request)
        {
            await SendResponse(request.ChatId, MessageTemplate.Create().SetText("Undefined command"));
        }
    }
}
