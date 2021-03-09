using System;
using System.Collections.Generic;
using CoreLearning.DBLibrary.DTO_models;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Commands
{
    public class ShowFriedListCommand : IRequest<IEnumerable<FriendModel>>
    {
        public ShowFriedListCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId {get;}
    }
}