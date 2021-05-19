﻿namespace TelegramBotApi.Types
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Text.RegularExpressions;

#nullable disable
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    internal class Message
    {

        [JsonProperty("message_id")]
        public long Id { get; set; }

        public User From { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        public Chat Chat { get; set; }

        public string Text { get; set; }

        private readonly Regex _commandRegex = new(@"^/(\w*)$", RegexOptions.Compiled);

        public bool IsCommand()
        {
            return _commandRegex.IsMatch(Text);
        }

        public string GetCommand()
        {
            return _commandRegex.Match(Text).Groups[1].Value;
        }
    }

#nullable restore

}