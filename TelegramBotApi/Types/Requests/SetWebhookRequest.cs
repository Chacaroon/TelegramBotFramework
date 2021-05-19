namespace TelegramBotApi.Types.Requests
{
    using Newtonsoft.Json;

    internal class SetWebhookRequest : BaseRequest
    {
        public string Url { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] AllowedUpdates { get; set; }

        public SetWebhookRequest(string url, string[] allowedUpdates)
        {
            Url = url;
            AllowedUpdates = allowedUpdates;
        }
    }
}
