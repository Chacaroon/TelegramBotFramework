namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;
    using TelegramBotApi.Types.ReplyMarkup;

    public class PvmsCommand : CommandBase
    {
        public async Task Invoke(QueryRequest request)
        {
            var replyMarkup = new InlineKeyboardMarkup()
                .AddRow(new InlineKeyboardButton("Lab 2", callbackData: $"{GetCommandName<FileCommand>()}:fileId=pvms_lab2.pdf"));

            await EditMessageAsync(request.MessageId, MessageTemplate.Create()
                .SetText("Choose your lab:")
                .SetMarkup(replyMarkup));

            await AnswerCallbackQueryAsync(request.QueryId);
        }
    }
}
