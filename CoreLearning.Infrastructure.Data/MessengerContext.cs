using CoreLearning.DBLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreLearning.Infrastructure.Data
{
    public class MessengerContext : DbContext
    {
        public MessengerContext(DbContextOptions<MessengerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Correspondence> Correspondences {get; set;}
        public DbSet<Message> Messages {get; set;}
        //public DbSet<Friend> Friends { get; set; }
        //public DbSet<FriendshipRequest> FriendshipRequests { get; set; }
        //public DbSet<BlockedUser> BlockedUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(c => c.Chats)
                        .WithMany(s => s.Users)
                        .UsingEntity(j => j.ToTable("Users_Chats"));
        }
    }
}