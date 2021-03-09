using System;
using System.Collections.Generic;
using CoreLearning.DBLibrary.DTO_models;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Commands
{
    public class ShowInboxApplicationListCommand : IRequest<IEnumerable<FriendModel>>
    {
        public ShowInboxApplicationListCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId {get;}
    }
}