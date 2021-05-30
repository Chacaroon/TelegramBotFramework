namespace TelegramBotApi.Services
{
    using System.Linq;
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Services.Abstraction;

    internal class CommandInvoker : ICommandInvoker
    {
        Task ICommandInvoker.InvokeCommand(CommandBase command, Request request)
        {
            command.TelegramBot.EnhanceWithRequest(request);

            var method = FindCommandEntryPoint(command, request);

            return (Task)method.Invoke(
                command,
                BindingFlags.Public | BindingFlags.Instance,
                null,
                method.GetParameters().Length == 0
                    ? null
                    : new object?[] { request },
                null)!;
        }

        private MethodInfo FindCommandEntryPoint(CommandBase command, Request request)
        {
            var methods = command.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.Name == "Invoke")
                .Where(x =>
                {
                    var parameters = x.GetParameters();

                    if (!parameters.Any())
                        return true;

                    return parameters.Length == 1 && parameters.Single().ParameterType == request.GetType();
                })
                .ToArray();

            if (!methods.Any())
            {
                // TODO: Throw proper exception
                throw new Exception();
            }

            if (methods.Length > 1)
            {
                // TODO: Throw proper exception
                throw new Exception();
            }

            var method = methods.Single();

            return method;
        }
    }
}
