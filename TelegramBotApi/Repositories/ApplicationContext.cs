namespace TelegramBotApi.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using TelegramBotApi.Repositories.Models;

    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<ChatState>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(x => x.ChatState)
                .WithOne(x => x.User)
                .HasForeignKey<ApplicationUser>(x => x.Id);
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ChatState> ChatStates { get; set; }
    }
}
