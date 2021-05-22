namespace TelegramBotApi.Types.ReplyMarkup
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    #nullable disable

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CallbackQuery
    {
        public string Id { get; set; }

        public User From { get; set; }
        public Message Message { get; set; }
        public string Data { get; set; }
    }

    #nullable restore
}