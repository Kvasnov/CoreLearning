using CoreLearning.DBLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreLearning.Infrastructure.Data
{
    public class UserContext : DbContext
    {
        public UserContext( DbContextOptions< UserContext > options ) : base( options )
        {
            Database.EnsureCreated();
        }

        public DbSet< User > Users { get; set; }
        //public DbSet<AuthenticationData> AuthenticationDatas { get; set; }
        //public DbSet<Chat> Chats { get; set; }
        //public DbSet<Message> Messages { get; set; }
        //public DbSet<Friend> Friends { get; set; }
        //public DbSet<FriendshipRequest> FriendshipRequests { get; set; }
        //public DbSet<BlockedUser> BlockedUsers { get; set; }
    }
}