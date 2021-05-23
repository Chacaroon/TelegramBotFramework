namespace TelegramBotApi.Types
{
#nullable disable

    internal class TelegramBotSettings
    {
        public string WebhookUri { get; set; }

        public string BotAccessToken { get; set; }

        public string[] AllowedUpdates { get; set; }
    }

#nullable restore
}
