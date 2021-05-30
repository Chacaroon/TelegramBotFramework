namespace TelegramBotApi.Models.Update
{
#nullable disable

    public class MessageRequest : Request
    {
        public int MessageId { get; set; }

        public string Text { get; set; }
    }

#nullable restore
}
