﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;

namespace TelegramBotApi.Types.Requests
{
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    internal class BaseRequest
    {
        public StringContent ToHttpContent()
        {
            return new(
                JsonConvert.SerializeObject(this),
                Encoding.UTF8,
                "application/json");
        }
    }
}