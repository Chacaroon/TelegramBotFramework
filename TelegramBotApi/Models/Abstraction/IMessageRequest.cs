namespace TelegramBotApi.Models.Abstraction
{
    public interface IMessageRequest : IRequest
    {
        IQuery Query { get; set; }
    }
}
