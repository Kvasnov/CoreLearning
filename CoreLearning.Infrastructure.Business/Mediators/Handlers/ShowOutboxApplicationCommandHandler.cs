using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Commands;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class ShowOutboxApplicationCommandHandler : IRequestHandler<ShowOutboxApplicationCommand, IEnumerable<FriendModel>>
    {
        public ShowOutboxApplicationCommandHandler(IFriendshipRepository repository)
        {
            this.repository = repository;
        }

        private readonly IFriendshipRepository repository;

        public async Task<IEnumerable<FriendModel>> Handle(ShowOutboxApplicationCommand request, CancellationToken cancellationToken)
        {
            return await repository.GetOutboxFriendshipListAsync(request.UserId);
        }
    }
}