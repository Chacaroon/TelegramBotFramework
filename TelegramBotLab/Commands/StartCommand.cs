namespace TelegramBotLab.Commands
{
    using System.Threading.Tasks;
    using TelegramBotApi;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Abstraction;
    using TelegramBotApi.Repositories.Abstraction;
    using TelegramBotApi.Repositories.Models;
    using TelegramBotApi.Types;

    public class StartCommand : BaseCommand, ICommand
    {
        private readonly IUserRepository _userRepository;

        public StartCommand(ITelegramBot telegramBot,
            IUserRepository userRepository)
            : base(telegramBot)
        {
            _userRepository = userRepository;
        }

        public async Task Invoke(IRequest request)
        {
            var user = new ApplicationUser(request.ChatId);
            _userRepository.Add(user);

            await SendResponse(
                request.ChatId,
                MessageTemplate.Create()
                    .SetText("Hello!"));
        }
    }
}
