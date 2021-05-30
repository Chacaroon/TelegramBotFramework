namespace TelegramBotApi.Services.Abstraction
{
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;

    internal interface ICommandInvoker
    {
        internal Task InvokeCommand(CommandBase command, Request request);
    }
}