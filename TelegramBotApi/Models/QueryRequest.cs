namespace TelegramBotApi.Models
{
    using TelegramBotApi.Models.Abstraction;

    internal class QueryRequest : Request, IQueryRequest
    {
        public long MessageId { get; set; }
        public IQuery Query { get; set; }

        public QueryRequest(
            long chatId,
            long messageId,
            string text)
            : base(chatId, text)
        {
            MessageId = messageId;
            Query = new Query(text);
        }
    }
}
