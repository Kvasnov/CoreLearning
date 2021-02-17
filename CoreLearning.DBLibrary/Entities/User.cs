using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Chats = new Collection<Correspondence>();
        }

        public string Name {get; set;}
        public string LastName {get; set;}
        public string Nickname {get; set;}
        public string Login {get; set;}
        public string Password {get; set;}

        //public UserRole Role { get; set; }
        //public List<Friend> Friends { get; set; }
        //public List<BlockedUser> BlockedUsers { get; set; }
        public ICollection<Correspondence> Chats {get; set;}
        //public List<FriendshipRequest> FriendshipRequests { get; set; }
        //public PermissionToWrite WhoCanWrite { get; set; }
    }
}