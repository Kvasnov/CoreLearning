using System;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.Common;

namespace CoreLearning.DBLibrary.Interfaces.Repositories
{
    public interface ICorrespondenceRepository : IEntityRepository<Correspondence>
    {
        Task<string> FindChatAsync(string senderId, Guid receiverId);
        Task AddMessageAsync(string correspondenceId, Message message);
        Task<Correspondence> GetByIdAsync(Guid id);

    }
}