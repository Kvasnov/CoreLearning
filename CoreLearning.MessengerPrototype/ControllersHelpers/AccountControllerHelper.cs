using System;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using CoreLearning.DBLibrary.Interfaces.Repositories;

namespace CoreLearning.MessengerPrototype.ControllersHelpers
{
    public sealed class AccountControllerHelper : IAccountControllerHelper
    {
        public AccountControllerHelper(IUserRepository repository)
        {
            this.repository = repository;
        }

        private readonly IUserRepository repository;

        public async Task AddUserAsync(RegisterModel registerModel)
        {
            var user = new User
                       {
                           Login = registerModel.Email,
                           Password = registerModel.Password,
                           Name = registerModel.Name,
                           LastName = registerModel.LastName,
                           Nickname = registerModel.Nickname
                       };

            await repository.AddAsync(user);
        }

        public Guid GetUserId(string login)
        {
            return repository.GetUserByLoginAsync(login).Result.Id;
        }

        public async Task<bool> CheckUserIsCreated(string login, string password)
        {
            return await repository.CheckUserIsCreatedAsync(login, password);
        }

        public async Task SaveAsync()
        {
            await repository.SaveAsync();
        }
    }
}