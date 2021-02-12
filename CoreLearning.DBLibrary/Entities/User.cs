using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class User : BaseEntity
    {
        public string Name {get; set;}
        public string LastName {get; set;}
        public string Nickname {get; set;}
        public string Login {get; set;}
        public string Password {get; set;}

        //public AuthenticationData AuthenticationData { get; set; }
        //public UserRole Role { get; set; }
        //public List<Friend> Friends { get; set; }
        //public List<BlockedUser> BlockedUsers { get; set; }
        //public List<Chat> Chats { get; set; }
        //public List<FriendshipRequest> FriendshipRequests { get; set; }
        //public PermissionToWrite WhoCanWrite { get; set; }
    }
}