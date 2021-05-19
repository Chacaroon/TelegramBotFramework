namespace TelegramBotApi.Services
{
    using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types;

    internal class WebhookService : IWebhookService
    {
        private readonly IMessageService _messageService;
        private readonly ICallbackQueryService _callbackQueryService;

        public WebhookService(IMessageService messageService,
            ICallbackQueryService callbackQueryService)
        {
            _messageService = messageService;
            _callbackQueryService = callbackQueryService;
        }

        public void Process(Update update)
        {
            if (update.IsMessage())
            {
                _messageService.HandleRequest(update.Message!);
            }

            if (update.IsCallbackQuery())
            {
                _callbackQueryService.HandleRequest(update.CallbackQuery!);
            }
        }
    }
}
