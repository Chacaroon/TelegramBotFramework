namespace TelegramBotApi.Services
{
    using System.Collections.Generic;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Extensions;
    using TelegramBotApi.Services.Abstraction;

    internal class CommandResolver : ICommandResolver
    {
        private readonly IEnumerable<CommandBase> _commands;

        public CommandResolver(IEnumerable<CommandBase> commands)
        {
            _commands = commands;
        }

        public CommandBase? Resolve(string name)
        {
            return _commands.GetCommand(name);
        }

        public CommandBase ResolveOrDefault(string? name)
        {
            return _commands.GetCommand(name) ?? _commands.GetUndefinedCommand();
        }
    }
}
