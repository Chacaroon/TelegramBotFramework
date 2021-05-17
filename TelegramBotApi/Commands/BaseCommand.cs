namespace TelegramBotApi.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi;
    using TelegramBotApi.Types;
    using TelegramBotApi.Types.Abstraction;

    public abstract class BaseCommand
	{
		private readonly ITelegramBot _telegramBot;

        protected BaseCommand(ITelegramBot telegramBot)
		{
			_telegramBot = telegramBot;
		}

		protected async Task SendResponse(
			long chatId,
			MessageTemplate messageTemplate)
		{
			var res = await _telegramBot.SendMessageAsync(
				chatId,
				messageTemplate);

			res.EnsureSuccessStatusCode();
		}

		protected async Task SendResponse(
			long chatId,
			long messageId,
			MessageTemplate messageTemplate)
		{
			var res = await _telegramBot.EditMessageAsync(
				chatId,
				messageId,
				messageTemplate);

			res.EnsureSuccessStatusCode();
		}
	}
}
