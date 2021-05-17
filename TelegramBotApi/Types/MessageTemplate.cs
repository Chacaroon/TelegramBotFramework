namespace TelegramBotApi.Types
{
    using TelegramBotApi.Types.Abstraction;
    using TelegramBotApi.Types.ReplyMarkup;

    public class MessageTemplate
    {
        public string Text { get; set; }
        public ParseMode ParseMode { get; set; }
        public IReplyMarkup ReplyMarkup { get; set; }

        public static MessageTemplate Create() => new MessageTemplate();

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
}
