namespace TelegramBotApi.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using TelegramBotApi.Repositories.Abstraction;
    using TelegramBotApi.Repositories.Models;

    internal class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        protected override IQueryable<ApplicationUser> BaseQuery => base.BaseQuery
            .Include(x => x.ChatState);

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
