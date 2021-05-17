namespace TelegramBotApi.Models
{
    using TelegramBotApi.Models.Abstraction;

    internal class MessageRequest : Request, IMessageRequest
    {
        public IQuery Query { get; set; }

        public MessageRequest(
            long chatId,
            string text,
            string query)
            : base(chatId, text)
        {
            Query = new Query(query);
        }
    }
}
