namespace TelegramBotApi.Types.Requests
{
    using Newtonsoft.Json;
    using TelegramBotApi.Types.Abstraction;

    internal class SendMessageRequest : BaseRequest
    {
        public SendMessageRequest(long chatId, string text)
        {
            ChatId = chatId;
            Text = text;
        }

        public long ChatId { get; set; }

        public string Text { get; set; }
        
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? ParseMode
        {
            get => _parseMode;
            set =>
                _parseMode = value == Types.ParseMode.None.ToString()
                ? null
                : value;
        }
        
        public bool DisableWebPagePreview { get; set; }
        
        public bool DisableNotification { get; set; }
        
        public long ReplyToMessageId { get; set; }
        
        public IReplyMarkup? ReplyMarkup { get; set; }

        private string? _parseMode;
    }
}
