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
    using TelegramBotApi.Types.ReplyMarkup;

    public class SomeCommand : BaseCommand, ICommand
    {
        public SomeCommand(ITelegramBot telegramBot) : base(telegramBot)
        {
        }

        public async Task Invoke(IRequest request)
        {
            var markup = new InlineKeyboardMarkup().AddRow(
                new InlineKeyboardButton("Button 1", callbackData: "another:someParam=42"), new InlineKeyboardButton("Button 2", callbackData: "another:someParam=43")
            )
                .AddRow(new InlineKeyboardButton("Button 3", callbackData: "another:someParam=44"));

            var message = MessageTemplate.Create()
                .SetText("Some response")
                .SetMarkup(markup);

            await SendResponse(request.ChatId, message);
        }
    }
}
