using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces;

namespace CoreLearning.MessengerPrototype.ControllersHelpers
{
    public class AccountControllerHelper
    {
        public AccountControllerHelper(IUserRepository repository)
        {
            this.repository = repository;
        }

        private readonly IUserRepository repository;

        public void AddUser(RegisterModel registerModel)
        {
            var user = new User {Login = registerModel.Email, Password = registerModel.Password};
            repository.AddUser(user);
            repository.Save();
        }

        public bool CheckUserIsCreated(string login, string password)
        {
            return repository.CheckUserIsCreated(login, password);
        }
    }
}