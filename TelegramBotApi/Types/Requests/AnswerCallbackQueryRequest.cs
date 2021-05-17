using Newtonsoft.Json;

namespace TelegramBotApi.Types.Requests
{
    internal class AnswerCallbackQueryRequest : BaseRequest
    {
        public string CallbackQueryId { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Text { get; set; }

        public AnswerCallbackQueryRequest(string callbackQueryId)
        {
            CallbackQueryId = callbackQueryId;
        }
    }
}
