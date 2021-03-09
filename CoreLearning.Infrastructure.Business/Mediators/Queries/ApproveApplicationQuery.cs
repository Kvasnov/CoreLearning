using System;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Queries
{
    public class ApproveApplicationQuery : IRequest
    {
        public ApproveApplicationQuery(Guid userId, Guid friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }

        public Guid UserId {get;}
        public Guid FriendId {get;}
    }
}