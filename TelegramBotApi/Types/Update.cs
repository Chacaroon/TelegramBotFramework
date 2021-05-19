namespace TelegramBotApi.Types
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using TelegramBotApi.Types.ReplyMarkup;

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    internal class Update
    {
        [JsonProperty("update_id")]
        public long Id { get; set; }

        public Message? Message { get; set; }
        public CallbackQuery? CallbackQuery { get; set; }

        public bool IsMessage()
        {
            return Message != null;
        }

        public bool IsCallbackQuery()
        {
            return CallbackQuery != null;
        }
    }
}
