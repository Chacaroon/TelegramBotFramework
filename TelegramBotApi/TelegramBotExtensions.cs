namespace TelegramBotApi
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Services;
    using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types;

    public static class TelegramBotExtensions
    {
        private static readonly string _webhookEndpoint = "/api/webhook";
        private static readonly string _telegramApiBase = "https://api.telegram.org/bot";

        public static IServiceCollection AddTelegramBot(this IServiceCollection services)
        {
            services.AddScoped<ITelegramBot, TelegramBot>();

            services.AddHttpClient<ITelegramBot, TelegramBot>(configureClient =>
            {
                var botAccessToken = services.BuildServiceProvider().GetRequiredService<IConfiguration>()["TelegramBotSettings:BotAccessToken"];

                configureClient.BaseAddress = new Uri($"{_telegramApiBase}{botAccessToken}/");
            });

            services.AddScoped<IWebhookService, WebhookService>();

            services.AddSingleton<ICommandResolver, CommandResolver>();

            var commands = Assembly.GetCallingAssembly().GetTypes()
                .Where(x => x.IsClass && x.IsPublic && x.IsSubclassOf(typeof(CommandBase)))
                .ToArray();

            foreach (var command in commands)
            {
                services.AddTransient(typeof(CommandBase), sp =>
                {
                    var commandInstance = (CommandBase)Activator.CreateInstance(command)!;
                    typeof(CommandBase)
                        .GetField("_telegramBot", BindingFlags.NonPublic | BindingFlags.Instance)!
                        .SetValue(commandInstance, sp.GetRequiredService<ITelegramBot>());

                    return commandInstance;
                });
            }

            return services;
        }

        public static IApplicationBuilder UseTelegramBot(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>().GetSection(nameof(TelegramBotSettings)).Get<TelegramBotSettings>();
            var telegramBot = app.ApplicationServices.GetRequiredService<ITelegramBot>();

            telegramBot.SetWebhook(
                $"{configuration.WebhookUri.Trim('/')}{_webhookEndpoint}",
                configuration.AllowedUpdates);

            app.Map(_webhookEndpoint, applicationBuilder =>
            {
                applicationBuilder.Run(async context =>
                {
                    if (context.Request.Method != "POST")
                    {
                        context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                        return;
                    }

                    var webhookService = context.RequestServices.GetRequiredService<IWebhookService>();

                    using var stream = new StreamReader(context.Request.Body);
                    var update = JsonConvert.DeserializeObject<Update>(await stream.ReadToEndAsync());

                    await webhookService.Process(update!);
                });
            });

            return app;
        }
    }
}
