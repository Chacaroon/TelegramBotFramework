using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TelegramBotApi.Types.ReplyMarkup
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class AnswerCallbackQuery
    {
        public string CallbackQueryId { get; set; }
        public string Text { get; set; }
    }
}
