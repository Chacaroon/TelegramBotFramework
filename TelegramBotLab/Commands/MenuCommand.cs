namespace TelegramBotLab.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;
    using TelegramBotApi.Types.Abstraction;
    using TelegramBotApi.Types.ReplyMarkup;

    public class MenuCommand : CommandBase
    {
        private readonly Dictionary<string, string> _subjects = new()
        {
            { "pvms", "ПВМС" },
            { "apz", "АаааааааПЗ" }
        };

        public async Task Invoke(MessageRequest request)
        {
            var replyMarkup = _subjects.Aggregate(
                new InlineKeyboardMarkup() as IReplyMarkup,
                (markup, pair) =>
                    markup.AddRow(new InlineKeyboardButton(pair.Value, callbackData: pair.Key)));

            await SendMessageAsync(MessageTemplate.Create()
                .SetText("First, choose the subject:")
                .SetMarkup(replyMarkup));
        }
    }
}
