namespace TelegramBotApi.Services
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Constants;
    using TelegramBotApi.Services.Abstraction;

    internal class CommandResolver : ICommandResolver
    {
        private readonly IServiceProvider _services;
        private readonly IReadOnlyDictionary<string, Type> _commandsDictionary;

        public CommandResolver(IServiceProvider services, 
            IReadOnlyDictionary<string, Type> commandsDictionary)
        {
            _services = services;
            _commandsDictionary = commandsDictionary;
        }

        public CommandBase? Resolve(string? name)
        {
            return _commandsDictionary.TryGetValue(name?.ToLower() ?? string.Empty, out var commandType)
                ? (CommandBase)_services.GetRequiredService(commandType)
                : null;
        }
    }
}
