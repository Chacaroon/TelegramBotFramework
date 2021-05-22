namespace TelegramBotApi.Types
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using TelegramBotApi.Types.ReplyMarkup;

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Update
    {
        [JsonProperty("update_id")]
        public long Id { get; set; }

        public Message? Message { get; set; }

        public CallbackQuery? CallbackQuery { get; set; }

        public bool IsMessage => Message != null;

        public bool IsCallbackQuery => CallbackQuery != null;
    }
}
