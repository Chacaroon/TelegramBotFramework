namespace TelegramBotApi.Models
{
    using TelegramBotApi.Models.Abstraction;

    internal class CommandRequest : Request, ICommandRequest
    {
        public CommandRequest(long chatId, string text)
            : base(chatId, text)
        { }
    }
}
