namespace TelegramBotApi.Repositories.Abstraction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TelegramBotApi.Repositories.Models;

    public interface IRepository<T> where T : Entity
    {
        void Add(T item);
        T FindById(long id);

        IQueryable<T> GetAll(Func<T, bool> predicate);
        IQueryable<T> GetAll();
        void AddRange(IEnumerable<T> item);
        void Update(T item);
        void Delete(T item);
        void DeleteById(long id);
        void DeleteRange(IEnumerable<T> item);
    }
}
