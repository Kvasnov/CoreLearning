using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.Common;

namespace CoreLearning.DBLibrary.Interfaces.Repositories
{
    public interface IFriendshipRepository : IEntityRepository<Friendship>
    {
        public Task<IQueryable<FriendModel>> GetFriendListAsync(Guid userId);
        public Task<IQueryable<FriendModel>> GetInboxFriendshipListAsync(Guid userId);
        public Task<IQueryable<FriendModel>> GetOutboxFriendshipListAsync(Guid userId);
        public Task ApproveApplicationAsync(Guid userId, Guid friendId);
        public Task RemoveFriendAsync(Guid userId, Guid friendId);
    }
}