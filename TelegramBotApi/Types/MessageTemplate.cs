namespace TelegramBotApi.Types
{
    using TelegramBotApi.Types.Abstraction;

#nullable disable

    public class MessageTemplate
    {
        public string Text { get; set; }

        public ParseMode ParseMode { get; set; }

        public IReplyMarkup ReplyMarkup { get; set; }

        public static MessageTemplate Create() => new();

        public MessageTemplate SetText(string text)
        {
            Text = text;
            return this;
        }

        public MessageTemplate SetParseMode(ParseMode parseMode)
        {
            ParseMode = parseMode;
            return this;
        }

        public MessageTemplate SetMarkup(IReplyMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }

#nullable restore

}
