namespace TelegramBotApi.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi.Models.Abstraction;

    public interface ICommand
    {
        Task Invoke(IRequest request);
    }
}
