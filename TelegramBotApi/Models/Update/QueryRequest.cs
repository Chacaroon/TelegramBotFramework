namespace TelegramBotApi.Models.Update
{
#nullable disable

    public class QueryRequest : Request
    {
        public string QueryId { get; set; }

        public long MessageId { get; set; }

        public Query Query { get; set; }
    }

    #nullable restore
}
