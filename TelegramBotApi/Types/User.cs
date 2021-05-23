namespace TelegramBotApi.Types
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

#nullable disable

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class User
    {
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }

#nullable restore

}