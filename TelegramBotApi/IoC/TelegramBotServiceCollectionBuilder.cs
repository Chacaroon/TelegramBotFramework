namespace TelegramBotApi.IoC
{
    using System;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Caching.Redis;
    using Microsoft.Extensions.DependencyInjection;

    public class TelegramBotServiceCollectionBuilder
    {
        private readonly IServiceCollection _services;

        internal TelegramBotServiceCollectionBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public TelegramBotServiceCollectionBuilder AddRedisChatStateStorage(Action<RedisCacheOptions> setupAction)
        {
            _services.AddDistributedRedisCache(setupAction);

            return this;
        }

        public TelegramBotServiceCollectionBuilder AddInMemoryChatStateStorage()
        {
            _services.AddDistributedMemoryCache();

            return this;
        }

        public TelegramBotServiceCollectionBuilder AddInMemoryChatStateStorage(Action<MemoryDistributedCacheOptions> setupAction)
        {
            _services.AddDistributedMemoryCache(setupAction);

            return this;
        }
    }
}
