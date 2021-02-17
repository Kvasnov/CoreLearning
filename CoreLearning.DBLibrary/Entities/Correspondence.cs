using System.Collections.Generic;
using System.Collections.ObjectModel;
using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class Correspondence : BaseEntity
    {
        public Correspondence()
        {
            Users = new Collection<User>();
            MessageHistory = new Collection<Message>();
        }

        public string Name {get; set;}
        public ICollection<User> Users {get; set;}
        public ICollection<Message> MessageHistory {get; set;}
    }
}