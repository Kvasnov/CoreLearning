using System;
using System.Collections.Generic;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Queries
{
    public class ShowAllChatQuery : IRequest<IEnumerable<Tuple<Guid, string>>>
    {
        public ShowAllChatQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId {get;}
    }
}