namespace TelegramBotApi.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using TelegramBotApi.Commands;
    using TelegramBotApi.Repositories;
    using TelegramBotApi.Repositories.Abstraction;
    using TelegramBotApi.Repositories.Models;
    using TelegramBotApi.Services;
    using TelegramBotApi.Services.Abstraction;
    using TelegramBotApi.Types;

    public static class TelegramBotExtensions
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services)
        {
            services.AddTransient<ITelegramBot, TelegramBot>();

            services.AddHttpClient<ITelegramBot, TelegramBot>(configureClient =>
            {
                configureClient.BaseAddress = new Uri(services.BuildServiceProvider().GetRequiredService<IConfiguration>()["TelegramBotSettings:ApiUri"]);
            });

            services.AddDbContext<ApplicationContext>(options => options
                .UseSqlServer(services.BuildServiceProvider().GetRequiredService<IConfiguration>()
                    .GetConnectionString("TelegramBot")));

            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<ICallbackQueryService, CallbackQueryService>();
            services.AddTransient<IWebhookService, WebhookService>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRepository<ChatState>, Repository<ChatState>>();

            var commands = Assembly.GetCallingAssembly().GetTypes()
                .Where(x => x.IsClass && x.IsPublic && x.GetInterface(nameof(ICommand)) != null)
                .ToArray();

            foreach (var command in commands)
            {
                services.AddTransient(typeof(ICommand), command);
            }

            return services;
        }

        public static IApplicationBuilder UseTelegramBot(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>().GetSection(nameof(TelegramBotSettings)).Get<TelegramBotSettings>();
            var telegramBot = app.ApplicationServices.GetRequiredService<ITelegramBot>();

            telegramBot.SetWebhook(
                configuration.WebhookUri,
                configuration.AllowedUpdates);

            app.Map("/api/webhook", (applicationBuilder) =>
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

                    webhookService.Process(update);
                });
            });

            return app;
        }
    }
}
