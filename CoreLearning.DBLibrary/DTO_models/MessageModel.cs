using System;

namespace CoreLearning.DBLibrary.DTO_models
{
    public class MessageModel
    {
        public Guid SenderUserId {get; set;}
        public string Description {get; set;}
    }
}