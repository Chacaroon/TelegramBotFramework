namespace TelegramBotApi.Types.ReplyMarkup
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

#nullable disable

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class AnswerCallbackQuery
    {
        public string CallbackQueryId { get; set; }
        public string Text { get; set; }
    }

#nullable restore

}
