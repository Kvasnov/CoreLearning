using System;
using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class BlockedUser : BaseEntity
    {
        public Guid UserId {get; set;}
    }
}