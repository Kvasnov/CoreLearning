using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces;

namespace CoreLearning.MessengerPrototype.ControllersHelpers
{
    public sealed class ChatControllerHelper
    {
        public ChatControllerHelper(IChatsRepository chatRepository, IUserRepository userRepository)
        {
            this.chatRepository = chatRepository;
            this.userRepository = userRepository;
        }

        private readonly IChatsRepository chatRepository;
        private readonly IUserRepository userRepository;

        public async Task<string> FindChatAsync(string senderId, string receiverId)
        {
            return await chatRepository.FindChatAsync(senderId, receiverId);
        }

        public async Task<string> CreateChatAsync(string senderId, string receiverId)
        {
            var sender = await userRepository.GetByIdAsync(Guid.Parse(senderId));
            var receiver = await userRepository.GetByIdAsync(Guid.Parse(receiverId));
            var chat = new Chat {Users = new List<User> {sender, receiver}, Name = $"{receiver.Name} {receiver.LastName}"};
            sender.Chats.Add(chat);
            receiver.Chats.Add(chat);
            await chatRepository.AddAsync(chat);
            await SaveAsync();

            return chat.Id.ToString();
        }

        public async Task SaveAsync()
        {
            await chatRepository.SaveAsync();
        }

        public async Task SendMessageAsync(string chatId, string senderId, string description)
        {
            await chatRepository.AddMessageAsync(chatId, new Message {SenderUserId = Guid.Parse(senderId), Description = description});
        }

        public async Task<ChatModel> ShowChatAsync(string chatId)
        {
            var chat = await chatRepository.GetByIdAsync(Guid.Parse(chatId));
            var chatDto = new ChatModel {Name = chat.Name};
            chat.MessageHistory.ForEach(message => chatDto.MessageHistory.Add(new MessageModel {Description = message.Description, SenderUserId = message.SenderUserId}));

            return chatDto;
        }

        public async Task<IEnumerable<Tuple<Guid, string>>> ShowAllChatsAsync(string senderId)
        {
            var user = await userRepository.GetByIdAsync(Guid.Parse(senderId));

            return user.Chats.Select(chat => new Tuple<Guid, string>(chat.Id, chat.Name));
        }
    }
}