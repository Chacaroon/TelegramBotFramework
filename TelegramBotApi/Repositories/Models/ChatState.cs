namespace TelegramBotApi.Repositories.Models
{
    using TelegramBotApi.Extensions;

    public class ChatState : Entity
    {
        public ApplicationUser User { get; set; }

        public bool IsWaitingFor
        {
            get => _isWaitingFor;
            set
            {
                if (!value)
                {
                    _waitingFor = null;
                }

                _isWaitingFor = value;
            }
        }
        public string WaitingFor
        {
            get => _waitingFor;
            set
            {
                IsWaitingFor = !value.IsNullOrEmpty();
                _waitingFor = value;
            }
        }

        private bool _isWaitingFor;
        private string _waitingFor;
    }
}
