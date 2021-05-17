namespace TelegramBotApi.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TelegramBotApi;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Extensions;
    using TelegramBotApi.Models;
    using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types.ReplyMarkup;

    public class CallbackQueryService : ICallbackQueryService
    {
        private readonly IEnumerable<ICommand> _commands;
        private readonly ITelegramBot _telegramBot;

        public CallbackQueryService(
            IEnumerable<ICommand> commands,
            ITelegramBot telegramBot)
        {
            _commands = commands;
            _telegramBot = telegramBot;
        }

        public async Task HandleRequest(CallbackQuery callbackQuery)
        {
            var request = new QueryRequest(
                callbackQuery.Message.Chat.Id,
                callbackQuery.Message.Id,
                callbackQuery.Data);

            var command = _commands.GetCommandOrDefault(request.Query.GetCommand());

            try
            {
                await command.Invoke(request);
            }
            catch
            {
                await _commands.GetErrorCommand().Invoke(request);
            }
            finally
            {
                await _telegramBot.AnswerCallbackQuery(callbackQuery.Id);
            }
        }
    }
}
