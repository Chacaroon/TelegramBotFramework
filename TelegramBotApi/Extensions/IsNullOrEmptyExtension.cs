namespace TelegramBotApi.Extensions
{
    public static class IsNullOrEmptyExtension
    {
        public static bool IsNullOrEmpty(this string item)
        {
            if (item.Trim() == "")
            {
                return true;
            }

            return item == null;
        }

        public static bool IsNullOrEmpty(this object item)
        {
            return item == null;
        }
    }
}
