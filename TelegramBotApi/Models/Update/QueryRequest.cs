namespace TelegramBotApi.Models.Update
{
    using TelegramBotApi.Types;

    #nullable disable

    public class QueryRequest : Request
    {
        public long MessageId { get; set; }

        public Query Query { get; set; }

        public static QueryRequest FromUpdate(Update update) =>
            new()
            {
                ChatId = update.CallbackQuery!.Message.Chat.Id,
                MessageId = update.CallbackQuery!.Message.Chat.Id,
                Query = new Query(update.CallbackQuery!.Data)
            };
    }

    #nullable restore
}
