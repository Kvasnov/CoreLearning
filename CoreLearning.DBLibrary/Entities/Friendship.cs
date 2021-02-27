using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLearning.DBLibrary.Entities
{
    public class Friendship
    {
        public virtual User Friend {get; set;}
        public virtual User FriendWith {get; set;}

        [ForeignKey(nameof( Friend ))]
        public Guid FriendId {get; set;}

        [ForeignKey(nameof( FriendWith ))]
        public Guid FriendWithId {get; set;}

        public bool AreTheyFriends {get; set;}
        public bool IsInboxFriendRequest {get; set;}
        public DateTime Created {get; set;}
    }
}