namespace TelegramBotApi.Models.ChatState
{
    public class ChatState
    {
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
        public string? WaitingFor
        {
            get => _waitingFor;
            set
            {
                IsWaitingFor = value != null;
                _waitingFor = value;
            }
        }

        private bool _isWaitingFor;
        private string? _waitingFor;
    }
}
