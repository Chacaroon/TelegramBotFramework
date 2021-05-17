namespace TelegramBotApi.Models.Abstraction
{
    using System.Collections.Generic;

    public interface IQuery
    {
        string GetCommand();
        Dictionary<string, string> GetQueryParams();
        string GetQueryParam(string param);
    }
}
