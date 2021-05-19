namespace TelegramBotApi.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Extensions;
    using TelegramBotApi.Models;
    using TelegramBotApi.Repositories.Abstraction;
    using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types;

    internal class MessageService : IMessageService
    {
        private readonly IEnumerable<ICommand> _commands;
        private readonly IUserRepository _userRepository;

        public MessageService(
            IEnumerable<ICommand> commands,
            IUserRepository userRepository)
        {
            _commands = commands;
            _userRepository = userRepository;
        }

        public async Task HandleRequest(Message message)
        {
            if (message.IsCommand())
            {
                await ProcessAsCommand(message);
                return;
            }

            await ProcessAsText(message);
        }

        private async Task ProcessAsCommand(Message message)
        {
            var command = _commands.GetCommand(message.GetCommand());

            var request = new CommandRequest(
                message.Chat.Id,
                message.Text);

            try
            {
                await command.Invoke(request);
            }
            catch
            {
                await _commands.GetErrorCommand().Invoke(request);
            }
        }

        private async Task ProcessAsText(Message message)
        {
            var user = _userRepository.GetAll(c => c.ChatId == message.Chat.Id).FirstOrDefault();

            if (user == null)
            {
                var tempRequest = new Request(message.Chat.Id, message.Text);

                await _commands.GetErrorCommand().Invoke(tempRequest);
                return;
            }

            if (!user.ChatState.IsWaitingFor)
            {
                var tempRequest = new Request(message.Chat.Id, message.Text);

                await _commands.GetUndefinedCommand().Invoke(tempRequest);
                return;
            }

            var request = new MessageRequest(
                message.Chat.Id,
                message.Text,
                user.ChatState.WaitingFor);

            var command = _commands.GetCommand(request.Query.GetCommand());

            try
            {
                await command.Invoke(request);
            }
            catch
            {
                await _commands.GetErrorCommand().Invoke(request);
            }
            finally
            {
                user.ChatState.IsWaitingFor = false;
                _userRepository.Update(user);
            }
        }
    }
}
