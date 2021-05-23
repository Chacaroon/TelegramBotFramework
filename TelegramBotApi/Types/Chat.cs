namespace TelegramBotApi.Types
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

#nullable disable

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Chat
    {
        public long Id { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

#nullable restore
}