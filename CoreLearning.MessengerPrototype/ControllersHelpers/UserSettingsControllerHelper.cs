using System;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces;

namespace CoreLearning.MessengerPrototype.ControllersHelpers
{
    public sealed class UserSettingsControllerHelper
    {
        public UserSettingsControllerHelper(IUserRepository repository)
        {
            this.repository = repository;
        }

        private readonly IUserRepository repository;

        public async Task<UserSettingsModel> GetUserSettingsAsync(string userId)
        {
            var user = await repository.GetByIdAsync(Guid.Parse(userId));

            return new UserSettingsModel {Login = user.Login, Name = user.Name, LastName = user.LastName, Nickname = user.Nickname};
        }

        public async Task<UserSettingsModel> SetUserSettingsAsync(string userId, ChangeUserSettingsModel newSettings)
        {
            var user = await repository.GetByIdAsync(Guid.Parse(userId));
            user.Login = newSettings.Login;
            user.Name = newSettings.Name;
            user.LastName = newSettings.LastName;
            user.Nickname = newSettings.Nickname;
            user.Password = newSettings.Password;
            await repository.UpdateSettings(user);

            return new UserSettingsModel {Login = user.Login, Name = user.Name, LastName = user.LastName, Nickname = user.Nickname};
        }

        public async Task SaveAsync()
        {
            await repository.SaveAsync();
        }
    }
}