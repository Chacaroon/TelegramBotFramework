namespace TelegramBotApi
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
    using TelegramBotApi.Models.ChatState;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;
    using TelegramBotApi.Types.Abstraction;
    using TelegramBotApi.Types.Requests;

    internal class TelegramBot : ITelegramBot
    {
        private readonly HttpClient _client;
        private readonly IDistributedCache _cache;
        private Request _request = null!;

        private const string CacheKeyPrefix = "tgbot";

        private long ChatId => _request.ChatId;
        private string CacheKey => GetCacheKey(_request.ChatId);

        public TelegramBot(HttpClient client,
            IDistributedCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task SetChatStateAsync(ChatState chatState)
        {
            if (!chatState.IsWaitingFor)
            {
                await _cache.RemoveAsync(CacheKey);
                return;
            }

            await _cache.SetAsync(CacheKey, Encoding.ASCII.GetBytes(chatState.WaitingFor!));
        }

        public async Task<ChatState> GetChatStateAsync()
        {
            var chatStateBytes = await _cache.GetAsync(CacheKey);

            if (chatStateBytes == null)
            {
                return new ChatState();
            } 

            var chatStateString = Encoding.ASCII.GetString(chatStateBytes);
            var chatState = new ChatState
            {
                WaitingFor = chatStateString
            };

            return chatState;
        }

        public Task<HttpResponseMessage> SetWebhookAsync(
            string webhookUri,
            string[] allowedUpdates = null!)
        {
            return MakeRequestAsync("setWebhook", new SetWebhookRequest(webhookUri, allowedUpdates));
        }

        public Task<HttpResponseMessage> SendMessageAsync(
            string text,
            ParseMode parseMode,
            bool disableWebPagePreview,
            bool disableNotification,
            long replyToMessageId,
            IReplyMarkup replyMarkup)
        {
            return MakeRequestAsync("sendMessage",
                           new SendMessageRequest(ChatId, text)
                           {
                               ParseMode = parseMode.ToString(),
                               DisableWebPagePreview = disableWebPagePreview,
                               DisableNotification = disableNotification,
                               ReplyToMessageId = replyToMessageId,
                               ReplyMarkup = replyMarkup
                           });
        }

        public Task<HttpResponseMessage> SendMessageAsync(
            MessageTemplate messageTemplate,
            bool disableWebPagePreview,
            bool disableNotification,
            long replyToMessageId)
        {
            return MakeRequestAsync("sendMessage",
                           new SendMessageRequest(ChatId, messageTemplate.Text)
                           {
                               ParseMode = messageTemplate.ParseMode.ToString(),
                               DisableWebPagePreview = disableWebPagePreview,
                               DisableNotification = disableNotification,
                               ReplyToMessageId = replyToMessageId,
                               ReplyMarkup = messageTemplate.ReplyMarkup
                           });
        }

        public Task<HttpResponseMessage> EditMessageAsync(
            long messageId,
            string text,
            ParseMode parseMode,
            bool disableWebPagePreview,
            IReplyMarkup replyMarkup)
        {
            return MakeRequestAsync("editMessageText",
                           new EditMessageRequest(ChatId, messageId, text)
                           {
                               ParseMode = parseMode.ToString(),
                               DisableWebPagePreview = disableWebPagePreview,
                               ReplyMarkup = replyMarkup
                           });
        }

        public Task<HttpResponseMessage> EditMessageAsync(
            long messageId,
            MessageTemplate messageTemplate,
            bool disableWebPagePreview)
        {
            return MakeRequestAsync("editMessageText",
                           new EditMessageRequest(ChatId, messageId, messageTemplate.Text)
                           {
                               ParseMode = messageTemplate.ParseMode.ToString(),
                               DisableWebPagePreview = disableWebPagePreview,
                               ReplyMarkup = messageTemplate.ReplyMarkup
                           });
        }

        public Task<HttpResponseMessage> AnswerCallbackQueryAsync(
            string callbackQueryId,
            string text)
        {
            return MakeRequestAsync("answerCallbackQuery",
                           new AnswerCallbackQueryRequest(callbackQueryId)
                           {
                               Text = text
                           });
        }

        internal void EnhanceWithRequest(Request request)
        {
            _request = request;
        }

        private Task<HttpResponseMessage> MakeRequestAsync(string url, BaseRequest obj)
        {
            return _client.PostAsync(url, obj.ToHttpContent());
        }

        private string GetCacheKey(long chatId) => $"{CacheKeyPrefix}:{chatId}";
    }
}
