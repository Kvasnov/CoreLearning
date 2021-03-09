using System;
using CoreLearning.DBLibrary.DTO_models;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Queries
{
    public class ShowChatQuery : IRequest<ChatModel>
    {
        public ShowChatQuery(Guid userId, Guid receiverUserId)
        {
            UserId = userId;
            ReceiverUserId = receiverUserId;
        }

        public Guid UserId {get;}
        public Guid ReceiverUserId {get;}
    }
}