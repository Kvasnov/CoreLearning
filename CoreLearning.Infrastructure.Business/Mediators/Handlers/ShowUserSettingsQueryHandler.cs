using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Queries;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class ShowUserSettingsQueryHandler : IRequestHandler<ShowUserSettingsQuery, UserSettingsModel>
    {
        public ShowUserSettingsQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        private readonly IUserRepository repository;

        public async Task<UserSettingsModel> Handle(ShowUserSettingsQuery request, CancellationToken cancellationToken)
        {
            var user = await repository.GetByIdAsync(request.Id);

            return new UserSettingsModel {Login = user.Login, Name = user.Name, LastName = user.LastName, Nickname = user.Nickname};
        }
    }
}