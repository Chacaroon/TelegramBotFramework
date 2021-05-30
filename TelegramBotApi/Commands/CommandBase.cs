namespace TelegramBotApi.Commands
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using TelegramBotApi;
    using TelegramBotApi.Types;

    public abstract class CommandBase
    {
        // The value will be set when resolving a specific command
        public ITelegramBot TelegramBot { get; internal set; } = null!;

        protected async Task SendMessageAsync(MessageTemplate messageTemplate)
        {
            var res = await TelegramBot.SendMessageAsync(
                messageTemplate);

            res.EnsureSuccessStatusCode();
        }

        protected async Task EditMessageAsync(long messageId, MessageTemplate messageTemplate)
        {
            var res = await TelegramBot.EditMessageAsync(
                messageId,
                messageTemplate);

            res.EnsureSuccessStatusCode();
        }

        protected async Task SendFileAsync(FileStream document)
        {
            var res = await TelegramBot.SendFileAsync(document);

            res.EnsureSuccessStatusCode();
        }

        protected async Task AnswerCallbackQueryAsync(string queryId)
        {
            var res = await TelegramBot.AnswerCallbackQueryAsync(queryId);

            res.EnsureSuccessStatusCode();
        }

        protected Task Redirect(string commandName)
        {
            return TelegramBot.InvokeCommand(commandName);
        }

        protected string GetCommandName<TCommand>() where TCommand : CommandBase
        {
            return typeof(TCommand).Name.Replace("Command", string.Empty, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
