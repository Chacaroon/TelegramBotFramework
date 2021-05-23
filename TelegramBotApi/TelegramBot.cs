namespace TelegramBotApi
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using TelegramBotApi.Types;
    using TelegramBotApi.Types.Abstraction;
    using TelegramBotApi.Types.Requests;
    using TelegramBotApi.Models.ChatState;

    internal class TelegramBot : ITelegramBot
    {
        private readonly HttpClient _client;

        public TelegramBot(HttpClient client)
        {
            _client = client;
        }

        private Task<HttpResponseMessage> MakeRequest(string url, BaseRequest obj)
        {
            return _client.PostAsync(url, obj.ToHttpContent());
        }

        public Task<ChatState> GetChatState(long chatId)
        {
            return Task.FromResult(new ChatState()
            {
                WaitingFor = "start1"
            });
        }

        public Task<HttpResponseMessage> SetWebhook(
            string webhookUri,
            string[] allowedUpdates = null!)
        {
            return MakeRequest("setWebhook", new SetWebhookRequest(webhookUri, allowedUpdates));
        }

        public Task<HttpResponseMessage> SendMessageAsync(
            long chatId,
            string text,
            ParseMode parseMode,
            bool disableWebPagePreview,
            bool disableNotification,
            long replyToMessageId,
            IReplyMarkup replyMarkup)
        {
            return MakeRequest("sendMessage",
                           new SendMessageRequest(chatId, text)
                           {
                               ParseMode = parseMode.ToString(),
                               DisableWebPagePreview = disableWebPagePreview,
                               DisableNotification = disableNotification,
                               ReplyToMessageId = replyToMessageId,
                               ReplyMarkup = replyMarkup
                           });
        }

        public Task<HttpResponseMessage> SendMessageAsync(
            long chatId,
            MessageTemplate messageTemplate,
            bool disableWebPagePreview,
            bool disableNotification,
            long replyToMessageId)
        {
            return MakeRequest("sendMessage",
                           new SendMessageRequest(chatId, messageTemplate.Text)
                           {
                               ParseMode = messageTemplate.ParseMode.ToString(),
                               DisableWebPagePreview = disableWebPagePreview,
                               DisableNotification = disableNotification,
                               ReplyToMessageId = replyToMessageId,
                               ReplyMarkup = messageTemplate.ReplyMarkup
                           });
        }

        public Task<HttpResponseMessage> EditMessageAsync(
            long chatId,
            long messageId,
            string text,
            ParseMode parseMode,
            bool disableWebPagePreview,
            IReplyMarkup replyMarkup)
        {
            return MakeRequest("editMessageText",
                           new EditMessageRequest(chatId, messageId, text)
                           {
                               ParseMode = parseMode.ToString(),
                               DisableWebPagePreview = disableWebPagePreview,
                               ReplyMarkup = replyMarkup
                           });
        }

        public Task<HttpResponseMessage> EditMessageAsync(
            long chatId,
            long messageId,
            MessageTemplate messageTemplate,
            bool disableWebPagePreview)
        {
            return MakeRequest("editMessageText",
                           new EditMessageRequest(chatId, messageId, messageTemplate.Text)
                           {
                               ParseMode = messageTemplate.ParseMode.ToString(),
                               DisableWebPagePreview = disableWebPagePreview,
                               ReplyMarkup = messageTemplate.ReplyMarkup
                           });
        }

        public Task<HttpResponseMessage> AnswerCallbackQuery(
            string callbackQueryId,
            string text)
        {
            return MakeRequest("answerCallbackQuery",
                           new AnswerCallbackQueryRequest(callbackQueryId)
                           {
                               Text = text
                           });
        }
    }
}
