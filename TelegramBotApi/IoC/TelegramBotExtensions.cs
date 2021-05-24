namespace TelegramBotApi.IoC
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
    using TelegramBotApi.Services;
    using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types;

    public static class TelegramBotExtensions
    {
        private const string WebhookEndpointBase = "/api/webhook";
        private const string TelegramApiBase = "https://api.telegram.org/bot";
        private const string CommandSuffix = "Command";

        public static TelegramBotServiceCollectionBuilder AddTelegramBot(this IServiceCollection services)
        {
            RegisterServices(services);
            RegisterCommands(services, Assembly.GetCallingAssembly().GetTypes());

            return new TelegramBotServiceCollectionBuilder(services);
        }

        public static IApplicationBuilder UseTelegramBot(this IApplicationBuilder app)
        {
            var telegramBotSettings = app.ApplicationServices.GetRequiredService<IConfiguration>()
                .GetSection(nameof(TelegramBotSettings))
                .Get<TelegramBotSettings>();

            AddMiddleware(app, GetWebhookEndpoint(telegramBotSettings));
            SetWebhookPath(app, telegramBotSettings);

            return app;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ITelegramBot, TelegramBot>();

            services.AddHttpClient<ITelegramBot, TelegramBot>(configureClient =>
            {
                var botAccessToken = services.BuildServiceProvider().GetRequiredService<IConfiguration>()["TelegramBotSettings:BotAccessToken"];

                configureClient.BaseAddress = new Uri($"{TelegramApiBase}{botAccessToken}/");
            });

            services.AddScoped<IWebhookService, WebhookService>();

            services.AddHttpContextAccessor();
        }

        private static void RegisterCommands(IServiceCollection services, Type[] types)
        {
            var commandTypes = types
                .Where(x => x.IsClass
                            && x.IsPublic
                            && x.IsSubclassOf(typeof(CommandBase)))
                .ToArray();

            var commandsDictionary = commandTypes.ToDictionary(
                x => Regex
                    .Replace(x.Name, $"{CommandSuffix}$", string.Empty, RegexOptions.IgnoreCase)
                    .ToLower());

            services.AddTransient<ICommandResolver, CommandResolver>(sp =>
                new CommandResolver(sp.GetRequiredService<IServiceProvider>(), commandsDictionary));

            foreach (var commandType in commandTypes)
            {
                services.AddTransient(commandType, commandType);
            }
        }

        private static void AddMiddleware(IApplicationBuilder app, string webhookEndpoint)
        {
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
        }

        private static void SetWebhookPath(IApplicationBuilder app, TelegramBotSettings telegramBotSettings)
        {
            var telegramBot = app.ApplicationServices.GetRequiredService<ITelegramBot>();
            var webhookUri = $"{telegramBotSettings.WebhookUri.Trim('/')}{GetWebhookEndpoint(telegramBotSettings)}";

            telegramBot.SetWebhookAsync(webhookUri, telegramBotSettings.AllowedUpdates);

        }

        private static string GetWebhookEndpoint(TelegramBotSettings telegramBotSettings)
        {
            return $"{WebhookEndpointBase}/{telegramBotSettings.BotAccessToken}";
        }
    }
}
