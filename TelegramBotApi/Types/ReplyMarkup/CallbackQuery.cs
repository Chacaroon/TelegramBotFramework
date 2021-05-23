namespace TelegramBotApi.Types.ReplyMarkup
{
    using System.Text.RegularExpressions;
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

        #region Helpers

        private readonly Regex _commandRegex = new(@"^([A-z]+):?", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public string GetCommand()
        {
            if (string.IsNullOrEmpty(Data))
            {
                return "";
            }

            var match = _commandRegex.Match(Data);

            return match.Groups[1].Value;
        }

        #endregion
    }

#nullable restore
}