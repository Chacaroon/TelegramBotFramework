namespace TelegramBotApi.Extensions
{
    internal static class IsNullOrEmptyExtension
    {
        public static bool IsNullOrEmpty(this string item)
        {
            return string.IsNullOrEmpty(item);
        }

        public static bool IsNullOrEmpty(this object item)
        {
            return item == null;
        }
    }
}
