using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces;
using CoreLearning.Infrastructure.Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CoreLearning.Infrastructure.Data.Repositories
{
    public sealed class ChatsRepository : BaseRepository<Chat>, IChatsRepository
    {
        public ChatsRepository(MessengerContext context) : base(context)
        {
        }

        public async Task<string> FindChatAsync(string senderId, string receiverId)
        {
            var chat = await context.Chats
                                    .Where(ch => ch.Users.Any(user => user.Id.ToString() == senderId))
                                    .FirstOrDefaultAsync(ch => ch.Users.Any(user => user.Id.ToString() == receiverId));

            return chat?.Id.ToString();
        }

        public async Task AddMessageAsync(string chatId, Message message)
        {
            await Task.CompletedTask;
            context.Chats.FirstOrDefault(chat => chat.Id == Guid.Parse(chatId))?.MessageHistory.Add(message);
        }

        public override async Task<Chat> GetByIdAsync(Guid id)
        {
            return await context.Chats.Include(chat => chat.Users).Include(chat => chat.MessageHistory).FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }
    }
}