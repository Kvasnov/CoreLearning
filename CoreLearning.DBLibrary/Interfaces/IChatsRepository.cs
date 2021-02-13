using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.Common;

namespace CoreLearning.DBLibrary.Interfaces
{
    public interface IChatsRepository : IEntityRepository<Chat>
    {
        Task<string> FindChatAsync(string senderId, string receiverId);
        Task AddMessageAsync(string chatId, Message message);
    }
}