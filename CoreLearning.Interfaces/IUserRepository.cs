using System.Collections.Generic;
using CoreLearning.DBLibrary.Entities;

namespace CoreLearning.Interfaces
{
    public interface IUserRepository
    {
        public List< User > GetUsers();
        public void AddUser( User user );
        public void Save();
    }
}