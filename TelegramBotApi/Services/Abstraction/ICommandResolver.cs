namespace TelegramBotApi.Services.Abstraction
{
    using TelegramBotApi.Commands;

    internal interface ICommandResolver
    {
        CommandBase ResolveOrDefault(string? name);
    }
}