using System.Collections.Generic;
using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class Chat : BaseEntity
    {
        public Chat()
        {
            Users = new List<User>();
            MessageHistory = new List<Message>();
        }

        public string Name {get; set;}
        public List<User> Users {get; set;}
        public List<Message> MessageHistory {get; set;}
    }
}