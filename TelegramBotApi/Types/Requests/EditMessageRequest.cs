using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TelegramBotApi.Types.Abstraction;

namespace TelegramBotApi.Types.Requests
{
    internal class EditMessageRequest : BaseRequest
    {
        public long ChatId { get; set; }
        public long MessageId { get; set; }

        public string Text { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ParseMode
        {
            get => _parseMode?.ToString();
            set =>
                _parseMode = value == Types.ParseMode.None.ToString()
                ? null
                : value;
        }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableWebPagePreview { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReplyMarkup ReplyMarkup { get; set; }

        private string _parseMode;

        public EditMessageRequest(long chatId, long messageId, string text)
        {
            ChatId = chatId;
            MessageId = messageId;
            Text = text;
        }
    }
}
