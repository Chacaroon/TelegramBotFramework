namespace TelegramBotApi.Commands
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using TelegramBotApi;
    using TelegramBotApi.Profiles;
    using TelegramBotApi.Types;

    public abstract class CommandBase
    {
        // The value will be set when resolving a specific command
        public ITelegramBot TelegramBot { get; internal set; } = null!;

        protected async Task SendResponseAsync(MessageTemplate messageTemplate)
        {
            var res = await TelegramBot.SendMessageAsync(
                messageTemplate);

            res.EnsureSuccessStatusCode();
        }

        protected async Task SendResponseAsync(long messageId, MessageTemplate messageTemplate)
        {
            var res = await TelegramBot.EditMessageAsync(
                messageId,
                messageTemplate);

            res.EnsureSuccessStatusCode();
        }
    }
}
