namespace TelegramBotApi.Services
{
    using System.Threading.Tasks;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types;

    internal class WebhookService : IWebhookService
    {
        private readonly ICommandResolver _commandResolver;
        private readonly ITelegramBot _telegramBot;

        public WebhookService(ICommandResolver commandResolver,
            ITelegramBot telegramBot)
        {
            _commandResolver = commandResolver;
            _telegramBot = telegramBot;
        }

        public async Task Process(Update update)
        {
            var command = update switch
            {
                { IsCallbackQuery: true } => _commandResolver.ResolveOrDefault(QueryRequest.FromUpdate(update).Query.GetCommand()),
                { IsMessage: true, Message: { IsCommand: true } } => _commandResolver.ResolveOrDefault(update.Message.GetCommand()),
                { IsMessage: true, Message: { IsCommand: false } } => _commandResolver.ResolveOrDefault(
                    (await _telegramBot.GetChatState(update.Message.Chat.Id)).WaitingFor),
                _ => _commandResolver.ResolveOrDefault(null)
            };

            await command.Invoke(update);
        }
    }
}
