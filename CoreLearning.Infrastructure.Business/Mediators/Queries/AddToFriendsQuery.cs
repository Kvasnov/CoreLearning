using System;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Queries
{
    public class AddToFriendsQuery : IRequest
    {
        public AddToFriendsQuery(Guid userId, Guid friendId)
        {
            UserId = userId;
            FriendId = friendId;
        }

        public Guid UserId {get;}
        public Guid FriendId {get;}
    }
}