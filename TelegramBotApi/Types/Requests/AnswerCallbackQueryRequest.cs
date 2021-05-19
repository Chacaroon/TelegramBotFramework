namespace TelegramBotApi.Types.Requests
{
    using Newtonsoft.Json;

    internal class AnswerCallbackQueryRequest : BaseRequest
    {
        public string CallbackQueryId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Text { get; set; }

        public AnswerCallbackQueryRequest(string callbackQueryId)
        {
            CallbackQueryId = callbackQueryId;
        }
    }
}
