namespace TelegramBotApi.Types.Abstraction
{
    public interface IReplyMarkup
    {
        IReplyMarkup AddRow(params IKeyboardButton[] buttons);
    }
}
