using System.Collections.Generic;
using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class Chat : BaseEntity
    {
        public List<User> Users {get; set;}
        public List<Message> MessageHistory {get; set;}
    }
}