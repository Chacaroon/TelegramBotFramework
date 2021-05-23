namespace TelegramBotApi
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Constants;
    using TelegramBotApi.Services;
    using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types;

    public static class TelegramBotExtensions
    {
        private const string WebhookEndpointBase = "/api/webhook";
        private const string TelegramApiBase = "https://api.telegram.org/bot";

        public static IServiceCollection AddTelegramBot(this IServiceCollection services)
        {
            services.AddScoped<ITelegramBot, TelegramBot>();

            services.AddHttpClient<ITelegramBot, TelegramBot>(configureClient =>
            {
                var botAccessToken = services.BuildServiceProvider().GetRequiredService<IConfiguration>()["TelegramBotSettings:BotAccessToken"];

                configureClient.BaseAddress = new Uri($"{TelegramApiBase}{botAccessToken}/");
            });

            services.AddScoped<IWebhookService, WebhookService>();


            var commandTypes = Assembly.GetCallingAssembly().GetTypes()
                .Where(x => x.IsClass
                            && x.IsPublic
                            && x.IsSubclassOf(typeof(CommandBase)))
                .ToArray();

            var commandsDictionary = commandTypes.ToDictionary(
                x => Regex
                    .Replace(x.Name, $"{InternalConstants.CommandSuffix}$", string.Empty, RegexOptions.IgnoreCase)
                    .ToLower());

            services.AddTransient<ICommandResolver, CommandResolver>(sp =>
                new CommandResolver(sp.GetRequiredService<IServiceProvider>(), commandsDictionary));

            foreach (var commandType in commandTypes)
            {
                services.AddTransient(commandType, sp =>
                {
                    var commandInstance = Activator.CreateInstance(commandType)!;
                    typeof(CommandBase)
                        .GetProperty("TelegramBot", BindingFlags.Instance | BindingFlags.NonPublic)!
                        .SetValue(commandInstance, sp.GetRequiredService<ITelegramBot>());

                    return commandInstance;
                });
            }

            return services;
        }

        public static IApplicationBuilder UseTelegramBot(this IApplicationBuilder app)
        {
            var telegramBot = app.ApplicationServices.GetRequiredService<ITelegramBot>();
            var telegramBotSettings = app.ApplicationServices.GetRequiredService<IConfiguration>()
                .GetSection(nameof(TelegramBotSettings))
                .Get<TelegramBotSettings>();

            var webhookEndpoint = $"{WebhookEndpointBase}/{telegramBotSettings.BotAccessToken}";

            telegramBot.SetWebhook(
                $"{telegramBotSettings.WebhookUri.Trim('/')}{webhookEndpoint}",
                telegramBotSettings.AllowedUpdates);

            app.Map(webhookEndpoint, applicationBuilder =>
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
