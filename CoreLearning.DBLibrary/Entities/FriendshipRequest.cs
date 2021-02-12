using System;
using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class FriendshipRequest : BaseEntity
    {
        public Guid SenderUserId {get; set;}
        public Guid RecipientUserId {get; set;}
        public bool IsAccepted {get; set;}
    }
}