using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Queries;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class ApproveApplicationQueryHandler : IRequestHandler<ApproveApplicationQuery>
    {
        public ApproveApplicationQueryHandler(IFriendshipRepository repository)
        {
            this.repository = repository;
        }

        private readonly IFriendshipRepository repository;

        public async Task<Unit> Handle(ApproveApplicationQuery request, CancellationToken cancellationToken)
        {
            await repository.ApproveApplicationAsync(request.UserId, request.FriendId);
            await repository.SaveAsync();

            return Unit.Value;
        }
    }
}