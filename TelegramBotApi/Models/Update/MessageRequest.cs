namespace TelegramBotApi.Models.Update
{
    using TelegramBotApi.Types;

#nullable disable

    public class MessageRequest : Request
    {
        public string Text { get; set; }

        public static MessageRequest FromUpdate(Update update) =>
            new()
            {
                ChatId = update.Message!.Chat.Id,
                Text = update.Message.Text
            };
    }

#nullable restore
}
