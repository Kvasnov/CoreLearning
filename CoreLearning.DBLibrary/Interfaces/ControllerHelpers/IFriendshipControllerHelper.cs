using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;

namespace CoreLearning.DBLibrary.Interfaces.ControllerHelpers
{
    public interface IFriendshipControllerHelper
    {
        Task AddToFriendsAsync(Guid userId, Guid friendId);
        Task<IEnumerable<FriendModel>> ShowFriedListAsync(Guid userId);
        Task<IEnumerable<FriendModel>> ShowInboxApplicationListAsync(Guid userId);
        Task<IEnumerable<FriendModel>> ShowOutboxApplicationListAsync(Guid userId);
        Task ApproveApplicationAsync(Guid userId, Guid friendId);
        Task RemoveFriendAsync(Guid userId, Guid friendId);
        Task SaveAsync();
    }
}