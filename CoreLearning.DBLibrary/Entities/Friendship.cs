using System;
using System.ComponentModel.DataAnnotations.Schema;
using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class Friendship : BaseEntity
    {
        [ForeignKey(nameof( User ))]
        public Guid UserId {get; set;}

        [ForeignKey(nameof( UserFriend ))]
        public Guid FriendId {get; set;}

        public User User {get; set;}
        public User UserFriend {get; set;}
        public bool IsFriend {get; set;}
        public bool Inbox {get; set;}
    }
}