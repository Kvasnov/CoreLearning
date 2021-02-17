using System;
using System.ComponentModel.DataAnnotations.Schema;
using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class Message : BaseEntity
    {
        public virtual User Sender {get; set;}
        public virtual Correspondence Correspondence {get; set;}

        [ForeignKey(nameof( Sender ))]
        public Guid SenderUserId {get; set;}

        //public Guid RecipientUserId {get; set;}
        [ForeignKey(nameof( Correspondence ))]
        public Guid ChatId {get; set;}

        //public bool IsRead {get; set;}
        public string Description {get; set;}
    }
}