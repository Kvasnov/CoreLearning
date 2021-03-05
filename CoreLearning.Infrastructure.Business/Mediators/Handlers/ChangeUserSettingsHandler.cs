using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Commands;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class ChangeUserSettingsHandler : IRequestHandler<ChangeUserSettingsCommand, UserSettingsModel>
    {
        public ChangeUserSettingsHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        private readonly IUserRepository repository;

        public async Task<UserSettingsModel> Handle(ChangeUserSettingsCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetByIdAsync(request.Id);
            user.Login = request.Settings.Login;
            user.Name = request.Settings.Name;
            user.LastName = request.Settings.LastName;
            user.Nickname = request.Settings.Nickname;
            user.Password = request.Settings.Password;
            await repository.UpdateSettings(user);
            await repository.SaveAsync();

            return new UserSettingsModel {Login = user.Login, Name = user.Name, LastName = user.LastName, Nickname = user.Nickname};
        }
    }
}