using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using CoreLearning.DBLibrary.Interfaces.Repositories;

namespace CoreLearning.MessengerPrototype.ControllersHelpers
{
    public sealed class CorrespondenceControllerHelper : ICorrespondenceControllerHelper
    {
        public CorrespondenceControllerHelper(ICorrespondenceRepository correspondenceRepository, IUserRepository userRepository)
        {
            this.correspondenceRepository = correspondenceRepository;
            this.userRepository = userRepository;
        }

        private readonly ICorrespondenceRepository correspondenceRepository;
        private readonly IUserRepository userRepository;

        public async Task<string> FindChatAsync(string senderId, Guid receiverId)
        {
            return await correspondenceRepository.FindChatAsync(senderId, receiverId);
        }

        public async Task<string> CreateChatAsync(string senderId, Guid receiverId)
        {
            var sender = await userRepository.GetByIdAsync(Guid.Parse(senderId));
            var receiver = await userRepository.GetByIdAsync(receiverId);
            var correspondence = new Correspondence {Users = new List<User> {sender, receiver}, Name = $"{receiver.Name} {receiver.LastName}"};
            sender.Chats.Add(correspondence);
            receiver.Chats.Add(correspondence);
            await correspondenceRepository.AddAsync(correspondence);
            await SaveAsync();

            return correspondence.Id.ToString();
        }

        public async Task SaveAsync()
        {
            await correspondenceRepository.SaveAsync();
        }

        public async Task SendMessageAsync(string correspondenceId, string senderId, string description)
        {
            await correspondenceRepository.AddMessageAsync(correspondenceId, new Message {SenderUserId = Guid.Parse(senderId), Description = description});
        }

        public async Task<ChatModel> ShowChatAsync(string correspondenceId)
        {
            var correspondence = await correspondenceRepository.GetByIdAsync(Guid.Parse(correspondenceId));
            var chatDto = new ChatModel {Name = correspondence.Name};
            correspondence.MessageHistory.ToList().ForEach(message => chatDto.MessageHistory.Add(new MessageModel {Description = message.Description, SenderUserId = message.SenderUserId}));

            return chatDto;
        }

        public async Task<IEnumerable<Tuple<Guid, string>>> ShowAllChatsAsync(Guid senderId)
        {
            var user = await userRepository.GetChatsByUserIdAsync(senderId);

            return user.Chats.Select(correspondence => new Tuple<Guid, string>(correspondence.Id, correspondence.Name));
        }
    }
}