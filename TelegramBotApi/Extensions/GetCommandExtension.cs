namespace TelegramBotApi.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using TelegramBotApi.Commands;

    internal static class GetCommandExtension
    {
        public static CommandBase? GetCommand(this IEnumerable<CommandBase> commands, string commandName)
        {
            var command = commands.FirstOrDefault(c => c.GetType().Name.IsMatch($"^(?i){commandName}command$"));

            return command;
        }

        public static CommandBase GetErrorCommand(this IEnumerable<CommandBase> commands)
        {
            return commands.First(c => c.GetType().Name.IsMatch("ErrorCommand"));
        }

        public static CommandBase GetUndefinedCommand(this IEnumerable<CommandBase> commands)
        {
            return commands.First(c => c.GetType().Name.IsMatch("UndefinedCommand"));

        }
    }
}
