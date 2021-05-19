using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TelegramBotApi.Types
{
#nullable disable
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    internal class Chat
    {
        public long Id { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
#nullable restore
}