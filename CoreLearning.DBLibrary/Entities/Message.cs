using System;

namespace CoreLearning.DBLibrary.Entities
{
    public class Message
    {
        public Guid Id {get; set;}
        public Guid SenderUserId {get; set;}
        public Guid RecipientUserId {get; set;}
        public Guid ChatId {get; set;}
        public bool IsRead {get; set;}
        public DateTime DispatchTime {get; set;}
        public string Description {get; set;}
    }
}