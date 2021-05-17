using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TelegramBotApi.Types.ReplyMarkup
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CallbackQuery
    {
        public string Id { get; set; }

        public User From { get; set; }
        public Message Message { get; set; }
        public string Data { get; set; }
    }
}