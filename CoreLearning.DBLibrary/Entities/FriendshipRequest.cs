using System;

namespace CoreLearning.DBLibrary.Entities
{
    public class FriendshipRequest
    {
        public Guid Id { get; set; }
        public Guid SenderUserId { get; set; }
        public Guid RecipientUserId { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime CreationTime { get; set; }
    }
}