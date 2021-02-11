using System;
using System.Collections.Generic;

namespace CoreLearning.DBLibrary.Entities
{
    public class Chat
    {
        public Guid Id {get; set;}
        public List<User> Users {get; set;}
        public DateTime CreationTime {get; set;}
        public List<Message> MessageHistory {get; set;}
    }
}