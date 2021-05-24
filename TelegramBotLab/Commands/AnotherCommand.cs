namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;

    public class AnotherCommand : CommandBase
    {
        public AnotherCommand(IDistributedCache cache)
        {
            
        }

        public async Task Invoke(QueryRequest request)
        {
            await SendResponseAsync(MessageTemplate.Create().SetText("Enter your name:"));
        }
    }
}
