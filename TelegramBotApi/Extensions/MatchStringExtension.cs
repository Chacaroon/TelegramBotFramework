namespace TelegramBotApi.Extensions
{
    using System.Text.RegularExpressions;

    internal static class MatchStringExtension
    {
        public static bool IsMatch(this string str, string pattern)
        {
            return new Regex(pattern).IsMatch(str);
        }
    }
}
