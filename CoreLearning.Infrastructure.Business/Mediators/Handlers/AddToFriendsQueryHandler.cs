using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Queries;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class AddToFriendsQueryHandler : IRequestHandler<AddToFriendsQuery>
    {
        public AddToFriendsQueryHandler(IFriendshipRepository repository)
        {
            this.repository = repository;
        }

        private readonly IFriendshipRepository repository;

        public async Task<Unit> Handle(AddToFriendsQuery request, CancellationToken cancellationToken)
        {
            await repository.AddAsync(new Friendship {FriendId = request.UserId, FriendWithId = request.FriendId});
            await repository.AddAsync(new Friendship {FriendId = request.FriendId, FriendWithId = request.UserId, IsInboxFriendRequest = true});
            await repository.SaveAsync();

            return Unit.Value;
        }
    }
}