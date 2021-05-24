namespace TelegramBotApi
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using TelegramBotApi.Models.ChatState;
using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;
    using TelegramBotApi.Types.Abstraction;

    public interface ITelegramBot
    {
        Task<ChatState> GetChatStateAsync();

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
        
        Task<HttpResponseMessage> AnswerCallbackQueryAsync(
            string callbackQueryId,
            string text = default!);

        internal void EnhanceWithRequest(Request request);
    }
}
