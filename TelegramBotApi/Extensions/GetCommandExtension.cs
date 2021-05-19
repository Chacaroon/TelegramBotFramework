namespace TelegramBotApi.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using TelegramBotApi.Commands;

    internal static class GetCommandExtension
    {
        public static ICommand GetCommand(this IEnumerable<ICommand> commands, string commandName)
        {
            var command = commands.FirstOrDefault(c => c.GetType().Name.IsMatch($"^(?i){commandName}command$"));

            return command ?? commands.GetUndefinedCommand();
        }

        public static ICommand GetErrorCommand(this IEnumerable<ICommand> commands)
        {
            return commands.First(c => c.GetType().Name.IsMatch("ErrorCommand"));
        }

        public static ICommand GetUndefinedCommand(this IEnumerable<ICommand> commands)
        {
            return commands.First(c => c.GetType().Name.IsMatch("UndefinedCommand"));

        }
    }
}
