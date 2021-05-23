namespace TelegramBotApi.Services
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using TelegramBotApi.Constants;
    using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types;

    internal class WebhookService : IWebhookService
    {
        private readonly ICommandResolver _commandResolver;
        private readonly ITelegramBot _telegramBot;
        private readonly ILogger<WebhookService> _logger;

        public WebhookService(ICommandResolver commandResolver,
            ITelegramBot telegramBot,
            ILogger<WebhookService> logger)
        {
            _commandResolver = commandResolver;
            _telegramBot = telegramBot;
            _logger = logger;
        }

        public async Task Process(Update update)
        {
            var updateType = update.IsCallbackQuery
                ? "callback query"
                : update.Message!.IsCommand
                    ? "command"
                    : "message";

            _logger.LogInformation("Start processing update. Update type is {updateType}", updateType);

            var commandName = update switch
            {
                { IsCallbackQuery: true } => update.CallbackQuery!.GetCommand(),
                { IsMessage: true, Message: { IsCommand: true } } => update.Message.GetCommand(),
                { IsMessage: true, Message: { IsCommand: false } } => (await _telegramBot.GetChatState(update.Message.Chat.Id)).WaitingFor,
                _ => null
            };

            _logger.LogInformation("Trying to resolve command with name {commandName}", commandName);

            var command = _commandResolver.Resolve(commandName);

            if (command == null)
            {
                _logger.LogWarning(
                    "Command with name {commandName} was not found. {undefinedCommand} command will be invoked",
                    commandName,
                    InternalConstants.UndefinedCommandName);

                command = _commandResolver.Resolve(InternalConstants.UndefinedCommandName)!;
            }

            try
            {
                _logger.LogInformation("Invoke {commandName} command", commandName);

                await command.Invoke(update);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error was occurred during invoke {commandName} command", commandName);

                await _commandResolver.Resolve(InternalConstants.ErrorCommandName)!.Invoke(update);
            }
        }
    }
}
