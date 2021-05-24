﻿namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;

    public class UndefinedCommand : CommandBase
    {
        public async Task Invoke(MessageRequest request)
        {
            await SendResponseAsync(MessageTemplate.Create().SetText("Undefined command"));
        }
    }
}
