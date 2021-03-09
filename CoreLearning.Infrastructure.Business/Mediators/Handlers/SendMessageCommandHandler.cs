using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Commands;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
    {
        public SendMessageCommandHandler(ICorrespondenceRepository correspondenceRepository, IUserRepository userRepository)
        {
            this.correspondenceRepository = correspondenceRepository;
            this.userRepository = userRepository;
        }

        private readonly ICorrespondenceRepository correspondenceRepository;
        private readonly IUserRepository userRepository;

        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var correspondenceId = await correspondenceRepository.FindChatAsync(request.UserId, request.ReceiverModel.ReceiverUserId) ?? await CreateChatAsync(request.UserId, request.ReceiverModel.ReceiverUserId);
            if (Guid.Parse(correspondenceId).Equals(Guid.Empty))
                correspondenceId = await correspondenceRepository.FindChatAsync(request.UserId, request.ReceiverModel.ReceiverUserId);

            await correspondenceRepository.AddMessageAsync(correspondenceId, new Message {SenderUserId = request.UserId, Description = request.ReceiverModel.Description});
            await correspondenceRepository.SaveAsync();

            return Unit.Value;
        }

        private async Task<string> CreateChatAsync(Guid senderId, Guid receiverId)
        {
            var sender = await userRepository.GetByIdAsync(senderId);
            var receiver = await userRepository.GetByIdAsync(receiverId);
            var correspondence = new Correspondence {Users = new List<User> {sender, receiver}, Name = $"{receiver.Name} {receiver.LastName}"};
            sender.Chats.Add(correspondence);
            receiver.Chats.Add(correspondence);
            await correspondenceRepository.AddAsync(correspondence);
            await correspondenceRepository.SaveAsync();

            return correspondence.Id.ToString();
        }
    }
}