namespace TelegramBotApi.Types.Requests
{
    using System.IO;
    using System.Net.Http;

    internal class SendFileRequest : BaseRequest
    {
        public SendFileRequest(long chatId, FileStream document)
        {
            ChatId = chatId;
            Document = document;
        }

        public long ChatId { get; set; }

        public FileStream Document { get; set; }

        public override HttpContent ToHttpContent()
        {
            var form = new MultipartFormDataContent
            {
                {new StringContent(ChatId.ToString()), "chat_id"},
                {new StreamContent(Document), "document", Path.GetFileName(Document.Name)}
            };

            return form;
        }
    }
}
