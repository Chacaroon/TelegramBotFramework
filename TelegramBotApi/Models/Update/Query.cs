namespace TelegramBotApi.Models.Update
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Query
    {
        private readonly string _text;

        private readonly Regex _commandParametersRegex = new(@"[A-z]+:?(?:([A-z\d]+)=([^,]+),?)*", RegexOptions.IgnoreCase);

        public Query(string text)
        {
            _text = text;
        }

        public Dictionary<string, string> GetQueryParams()
        {
            var match = _commandParametersRegex.Match(_text);

            return match.Groups[1].Captures
                .Select(x => x.Value)
                .Zip(match.Groups[2].Captures
                    .Select(x => x.Value))
                .ToDictionary(x => x.First, x => x.Second);
        }

        public string? GetQueryParam(string param)
        {
            return GetQueryParams().GetValueOrDefault(param);
        }
    }
}
