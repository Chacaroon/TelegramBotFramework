namespace TelegramBotApi.Services
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Extensions.Logging;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Profiles;
    using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types;

    internal class WebhookService : IWebhookService
    {
        private readonly ICommandResolver _commandResolver;
        private readonly ITelegramBot _telegramBot;
        private readonly IMapper _mapper;
        private readonly ILogger<WebhookService> _logger;

        private const string UndefinedCommandName = "undefined";
        private const string ErrorCommandName = "error";

        public WebhookService(ICommandResolver commandResolver,
            ITelegramBot telegramBot,
            ILogger<WebhookService> logger)
        {
            _commandResolver = commandResolver;
            _telegramBot = telegramBot;
            _logger = logger;

            _mapper = new Mapper(new MapperConfiguration(x => x.AddProfile<RequestProfile>()));
        }

        public async Task Process(Update update)
        {
            var updateType = update.IsCallbackQuery
                ? "callback query"
                : update.Message!.IsCommand
                    ? "command"
                    : "message";

            _logger.LogInformation("Start processing update. Update type is {updateType}", updateType);

            var request = MapUpdateToRequest(update);

            _telegramBot.EnhanceWithRequest(request);

            var commandName = update switch
            {
                { IsCallbackQuery: true } => update.CallbackQuery!.GetCommand(),
                { IsMessage: true, Message: { IsCommand: true } } => update.Message.GetCommand(),
                { IsMessage: true, Message: { IsCommand: false } } => (await _telegramBot.GetChatStateAsync()).WaitingFor,
                _ => null
            };

            _logger.LogInformation("Trying to resolve command with name {commandName}", commandName);

            var command = _commandResolver.Resolve(commandName);

            if (command == null)
            {
                _logger.LogWarning(
                    "Command with name {commandName} was not found. {undefinedCommand} command will be invoked",
                    commandName,
                    UndefinedCommandName);

                command = _commandResolver.Resolve(UndefinedCommandName)!;
            }

            try
            {
                _logger.LogInformation("Invoke {commandName} command", commandName);

                await InvokeCommand(command, request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error was occurred during invoke {commandName} command", commandName);

                await InvokeCommand(_commandResolver.Resolve(ErrorCommandName)!, request);
            }
        }

        private Task InvokeCommand(CommandBase command, Request request)
        {
            command.TelegramBot.EnhanceWithRequest(request);

            var method = FindCommandEntryPoint(command, request);

            return (Task)method.Invoke(
                command,
                BindingFlags.Public | BindingFlags.Instance,
                null,
                method.GetParameters().Length == 0
                    ? null
                    : new object?[] { request },
                null)!;
        }

        private MethodInfo FindCommandEntryPoint(CommandBase command, Request request)
        {
            var methods = command.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.Name == "Invoke")
                .Where(x =>
                {
                    var parameters = x.GetParameters();

                    if (!parameters.Any())
                        return true;

                    return parameters.Length == 1 && parameters.Single().ParameterType == request.GetType();
                })
                .ToArray();

            if (!methods.Any())
            {
                // TODO: Throw proper exception
                throw new Exception();
            }

            if (methods.Length > 1)
            {
                // TODO: Throw proper exception
                throw new Exception();
            }

            var method = methods.Single();

            return method;
        }

        private Request MapUpdateToRequest(Update update)
        {
            return (Request)_mapper.Map(update, typeof(Update), GetRequestType(update));
        }

        private Type GetRequestType(Update update)
        {
            return update switch
            {
                { IsMessage: true } => typeof(MessageRequest),
                { IsCallbackQuery: true } => typeof(QueryRequest),
                _ => throw new ArgumentOutOfRangeException(nameof(update), update, null)
            };
        }
    }
}
