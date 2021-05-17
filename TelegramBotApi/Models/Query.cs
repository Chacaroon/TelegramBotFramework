namespace TelegramBotApi.Models
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using TelegramBotApi.Extensions;
    using TelegramBotApi.Models.Abstraction;

    internal class Query : IQuery
    {
        private readonly string _text;

        public Query(string text)
        {
            _text = text;
        }

        public string GetCommand()
        {
            if (_text.IsNullOrEmpty())
            {
                return "";
            }

            Match match = new Regex(@"^(?i)([a-z]+):?").Match(_text);

            return match.Groups[1].Value;
        }

        public Dictionary<string, string> GetQueryParams()
        {
            Dictionary<string, string> _params = new Dictionary<string, string>();

            Match match = new Regex(@"(?i)[a-z]+:?(?:([a-z]+)=([^,]+),?)*").Match(_text);

            for (int i = 0; i < match.Groups[1].Captures.Count; i++)
            {
                _params.Add(match.Groups[1].Captures[i].Value,
                    match.Groups[2].Captures[i].Value);
            }

            return _params;
        }

        public string GetQueryParam(string param)
        {
            return GetQueryParams().GetValueOrDefault(param);
        }
    }
}
