namespace TelegramBotLab.Commands
{
using System.IO;
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;

    public class UndefinedCommand : CommandBase
    {
        public async Task Invoke(MessageRequest request)
        {
            var subject = SubjectsResolver.ResolveSubject(request.Text);
            var type = SubjectsResolver.ResolveType(request.Text);
            var number = SubjectsResolver.ResolveNumber(request.Text);

            var fileName = $"{subject}_{type}{number}.pdf";

            await using var document = new FileStream(
                $@"wwwroot\Labs\{fileName}",
                FileMode.Open,
                FileAccess.Read);

            await SendFileAsync(document);
        }
    }
}
