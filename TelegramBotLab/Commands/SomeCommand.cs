namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;
    using TelegramBotApi.Types.ReplyMarkup;

    public class SomeCommand : CommandBase
    {
        public async Task Invoke(MessageRequest request)
        {
            var markup = new InlineKeyboardMarkup().AddRow(
                new InlineKeyboardButton("Button 1", callbackData: "another:someParam=42"), new InlineKeyboardButton("Button 2", callbackData: "another:someParam=43")
            )
                .AddRow(new InlineKeyboardButton("Button 3", callbackData: "another:someParam=44"));

            var message = MessageTemplate.Create()
                .SetText("Some response")
                .SetMarkup(markup);

            await SendResponseAsync(message);
        }
    }
}
