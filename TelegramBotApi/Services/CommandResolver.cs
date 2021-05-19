namespace TelegramBotApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Extensions;

    internal class CommandResolver
    {
        private readonly IEnumerable<ICommand> _commands;

        public CommandResolver(IEnumerable<ICommand> commands)
        {
            _commands = commands;
        }

        public ICommand? Resolve(string name)
        {
            return _commands.GetCommand(name);
        }

        public ICommand ResolveOrDefault(string name)
        {
            return _commands.GetCommand(name) ?? _commands.GetUndefinedCommand();
        }
    }
}
