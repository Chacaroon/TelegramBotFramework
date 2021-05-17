namespace TelegramBotApi.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using TelegramBotApi.Repositories.Abstraction;
    using TelegramBotApi.Repositories.Models;

    public class Repository<T> : IRepository<T> where T : Entity
    {
        public Repository(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        protected DbContext DbContext { get; set; }
        protected virtual IQueryable<T> BaseQuery => DbContext.Set<T>();

        public virtual void Add(T item)
        {
            DbContext.Set<T>().Add(item);
            DbContext.SaveChanges();
        }

        public virtual T FindById(long id)
        {
            return BaseQuery.SingleOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return BaseQuery;
        }
        public virtual IQueryable<T> GetAll(Func<T, bool> predicate)
        {
            return BaseQuery.Where(predicate).AsQueryable();
        }

        public virtual void AddRange(IEnumerable<T> item)
        {
            DbContext.Set<T>().AddRange(item);
            DbContext.SaveChanges();
        }

        public virtual void Delete(T item)
        {

            DbContext.Set<T>().Remove(item);
            DbContext.SaveChanges();
        }

        public void DeleteById(long id)
        {
            var item = FindById(id);
            DbContext.Set<T>().Remove(item);
            DbContext.SaveChanges();
        }
        public virtual void DeleteRange(IEnumerable<T> items)
        {
            DbContext.Set<T>().RemoveRange(items);
            DbContext.SaveChanges();
        }

        public virtual void Update(T item)
        {
            DbContext.Entry(item).State = EntityState.Modified;
            DbContext.SaveChanges();
        }
    }
}
