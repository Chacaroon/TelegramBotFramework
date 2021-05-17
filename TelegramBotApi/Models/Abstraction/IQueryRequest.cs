namespace TelegramBotApi.Models.Abstraction
{
    public interface IQueryRequest : IRequest
    {
        long MessageId { get; set; }
        IQuery Query { get; set; }
    }
}
