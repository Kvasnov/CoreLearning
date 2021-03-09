using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Queries;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class ShowChatQueryHandler : IRequestHandler<ShowChatQuery, ChatModel>
    {
        public ShowChatQueryHandler(ICorrespondenceRepository correspondenceRepository)
        {
            this.correspondenceRepository = correspondenceRepository;
        }

        private readonly ICorrespondenceRepository correspondenceRepository;

        public async Task<ChatModel> Handle(ShowChatQuery request, CancellationToken cancellationToken)
        {
            var correspondenceId = await correspondenceRepository.FindChatAsync(request.UserId, request.ReceiverUserId);
            var correspondence = await correspondenceRepository.GetByIdAsync(Guid.Parse(correspondenceId));
            var chatDto = new ChatModel {Name = correspondence.Name};
            correspondence.MessageHistory.ToList().ForEach(message => chatDto.MessageHistory.Add(new MessageModel {Description = message.Description, SenderUserId = message.SenderUserId}));

            return chatDto;
        }
    }
}