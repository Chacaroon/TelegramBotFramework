namespace TelegramBotApi
{
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TelegramBotApi.Models.ChatState;
    using TelegramBotApi.Models.Update;
using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types;
    using TelegramBotApi.Types.Abstraction;

    public interface ITelegramBot
    {
        Task<ChatState> GetChatStateAsync(bool clearState = false);

        Task SetChatStateAsync(ChatState chatState);

        Task<HttpResponseMessage> SetWebhookAsync(
            string webhookUri,
            string[] allowedUpdates = default!);

        Task<HttpResponseMessage> SendMessageAsync(
            string text,
            ParseMode parseMode = default,
            bool disableWebPagePreview = default,
            bool disableNotification = default,
            long replyToMessageId = default,
            IReplyMarkup replyMarkup = default!);

        Task<HttpResponseMessage> SendMessageAsync(
            MessageTemplate messageTemplate,
            bool disableWebPagePreview = default,
            bool disableNotification = default,
            long replyToMessageId = default);

        Task<HttpResponseMessage> EditMessageAsync(
            long messageId,
            string text,
            ParseMode parseMode = default,
            bool disableWebPagePreview = default,
            IReplyMarkup replyMarkup = default!);

        Task<HttpResponseMessage> EditMessageAsync(
            long messageId,
            MessageTemplate messageTemplate,
            bool disableWebPagePreview = default);

        public Task<HttpResponseMessage> SendFileAsync(FileStream document);

        Task<HttpResponseMessage> AnswerCallbackQueryAsync(
            string callbackQueryId,
            string text = default!);

        Task InvokeCommand(string commandName);

        internal void EnhanceWithRequest(Request request);
    }
}
