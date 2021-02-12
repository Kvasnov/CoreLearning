using System;
using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class Message : BaseEntity
    {
        public Guid SenderUserId {get; set;}
        public Guid RecipientUserId {get; set;}
        public Guid ChatId {get; set;}
        public bool IsRead {get; set;}
        public string Description {get; set;}
    }
}