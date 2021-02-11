using System.Collections.Generic;
using CoreLearning.DBLibrary.Entities;

namespace CoreLearning.DBLibrary.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        void AddUser(User user);
        void Save();
        bool CheckUserIsCreated(string login, string password);
    }
}