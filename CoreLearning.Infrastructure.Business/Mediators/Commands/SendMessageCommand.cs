using System;
using CoreLearning.DBLibrary.DTO_models;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Commands
{
    public class SendMessageCommand : IRequest
    {
        public SendMessageCommand(ReceiverModel receiverModel, Guid userId)
        {
            ReceiverModel = receiverModel;
            UserId = userId;
        }

        public ReceiverModel ReceiverModel {get;}
        public Guid UserId {get;}
    }
}