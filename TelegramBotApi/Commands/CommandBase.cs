namespace TelegramBotApi.Commands
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using AutoMapper;
    using TelegramBotApi;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Profiles;
    using TelegramBotApi.Types;

    public abstract class CommandBase
    {
        // The value will be set when resolving a specific command
        public ITelegramBot TelegramBot { get; private set; } = null!;

        private readonly IMapper _mapper;

        protected CommandBase()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<RequestProfile>());
            _mapper = new Mapper(config);
        }

        protected async Task SendResponse(
            long chatId,
            MessageTemplate messageTemplate)
        {
            var res = await TelegramBot.SendMessageAsync(
                chatId,
                messageTemplate);

            res.EnsureSuccessStatusCode();
        }

        protected async Task SendResponse(
            long chatId,
            long messageId,
            MessageTemplate messageTemplate)
        {
            var res = await TelegramBot.EditMessageAsync(
                chatId,
                messageId,
                messageTemplate);

            res.EnsureSuccessStatusCode();
        }

        internal Task Invoke(Update update)
        {
            var requestType = GetRequestType(update);
            var methods = GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.Name == "Invoke")
                .Where(x =>
                {
                    var parameters = x.GetParameters();

                    if (!parameters.Any())
                        return true;
                    
                    return parameters.Length == 1 && parameters.Single().ParameterType == requestType;
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

            var requestModel = _mapper.Map(update, typeof(Update), requestType);

            return (Task)method.Invoke(
                this, 
                BindingFlags.Public | BindingFlags.Instance, 
                null, 
                method.GetParameters().Length == 0 
                    ? null
                    : new [] { requestModel }, 
                null)!;
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
