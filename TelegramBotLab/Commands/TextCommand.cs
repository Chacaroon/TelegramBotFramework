namespace TelegramBotLab.Commands
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;

    public class TextCommand : CommandBase
    {
        public async Task Invoke(MessageRequest request)
        {
            var subject = SubjectsResolver.ResolveSubject(request.Text);
            var type = SubjectsResolver.ResolveType(request.Text);
            var number = SubjectsResolver.ResolveNumber(request.Text);

            if (new[] { subject, type, number?.ToString() }.Any(x => x == null))
            {
                await SendMessageAsync(MessageTemplate.Create()
                    .SetText("What? 🧐"));
                return;
            }

            var fileName = $"{subject}_{type}{number}.pdf";

            await using var document = new FileStream(
                $@"wwwroot\Labs\{fileName}",
                FileMode.Open,
                FileAccess.Read);

            await SendFileAsync(document);
        }
    }
}
