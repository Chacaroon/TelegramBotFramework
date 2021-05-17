namespace TelegramBotLab.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TelegramBotApi;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Abstraction;
    using TelegramBotApi.Repositories.Abstraction;
    using TelegramBotApi.Repositories.Models;
    using TelegramBotApi.Types;

    public class AnotherCommand : BaseCommand, ICommand
    {
        private readonly IUserRepository _userRepository;

        public AnotherCommand(ITelegramBot telegramBot, IUserRepository userRepository) : base(telegramBot)
        {
            _userRepository = userRepository;
        }

        public async Task Invoke(IRequest request)
        {
            var query = request as IQueryRequest;

            var user = _userRepository.GetAll(x => x.ChatId == request.ChatId)
                .Include(x => x.ChatState).FirstOrDefault();

            user.ChatState.WaitingFor = "text";

            _userRepository.Update(user);

            await SendResponse(
                request.ChatId,
                query.MessageId,
                MessageTemplate.Create().SetText("Enter your name:"));
        }
    }
}
