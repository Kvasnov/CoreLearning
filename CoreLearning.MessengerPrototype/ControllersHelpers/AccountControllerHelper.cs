using System;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces;

namespace CoreLearning.MessengerPrototype.ControllersHelpers
{
    public sealed class AccountControllerHelper
    {
        public AccountControllerHelper(IUserRepository repository)
        {
            this.repository = repository;
        }

        private readonly IUserRepository repository;

        public async Task AddUserAsync(RegisterModel registerModel)
        {
            var user = new User {Login = registerModel.Email, Password = registerModel.Password};
            await repository.AddAsync(user);
        }

        public Guid GetUserId(string login)
        {
            return repository.GetkUserAsync(login).Result.Id;
        }

        public bool CheckUserIsCreated(string login, string password)
        {
            return repository.CheckUserIsCreatedAsync(login, password).Result;
        }

        public async Task SaveAsync()
        {
            await repository.SaveAsync();
        }
    }
}