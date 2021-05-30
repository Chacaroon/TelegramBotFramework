namespace TelegramBotLab.Commands
{
    using System.IO;
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;

    public class FileCommand : CommandBase
    {
        public async Task Invoke(QueryRequest request)
        {
            await using var document = new FileStream(
                $@"wwwroot\Labs\{request.Query.GetQueryParam("fileId")}", 
                FileMode.Open,
                FileAccess.Read);

            var message = MessageTemplate.Create()
                .SetText("Have fun");

            await SendMessageAsync(message);

            await AnswerCallbackQueryAsync(request.QueryId);

            await SendFileAsync(document);
        }
    }
}
