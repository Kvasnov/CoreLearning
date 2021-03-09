using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Queries;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class RemoveFriendQueryHandler : IRequestHandler<RemoveFriendQuery>
    {
        public RemoveFriendQueryHandler(IFriendshipRepository repository)
        {
            this.repository = repository;
        }

        private readonly IFriendshipRepository repository;

        public async Task<Unit> Handle(RemoveFriendQuery request, CancellationToken cancellationToken)
        {
            await repository.RemoveFriendAsync(request.UserId, request.FriendId);
            await repository.SaveAsync();

            return Unit.Value;
        }
    }
}