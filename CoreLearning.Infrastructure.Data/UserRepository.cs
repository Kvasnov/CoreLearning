using System.Collections.Generic;
using System.Linq;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces;

namespace CoreLearning.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        private readonly UserContext context;

        public List<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public bool CheckUserIsCreated(string login, string password)
        {
            return context.Users.FirstOrDefault(user => user.Login == login && user.Password == password) != null;
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}