using System;
using CoreLearning.DBLibrary.Common;

namespace CoreLearning.DBLibrary.Entities
{
    public class Friend : BaseEntity
    {
        public Guid UserId {get; set;}
    }
}