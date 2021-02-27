using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using CoreLearning.DBLibrary.Interfaces.Repositories;

namespace CoreLearning.MessengerPrototype.ControllersHelpers
{
    public class FriendshipControllerHelper : IFriendshipControllerHelper
    {
        public FriendshipControllerHelper(IFriendshipRepository repository)
        {
            this.repository = repository;
        }

        private readonly IFriendshipRepository repository;

        public async Task AddToFriendsAsync(Guid userId, Guid friendId)
        {
            await repository.AddAsync(new Friendship {FriendId = userId, FriendWithId = friendId});
            await repository.AddAsync(new Friendship {FriendId = friendId, FriendWithId = userId, IsInboxFriendRequest = true});
        }

        public async Task<IEnumerable<FriendModel>> ShowFriedListAsync(Guid userId)
        {
            return await repository.GetFriendListAsync(userId);
        }

        public async Task<IEnumerable<FriendModel>> ShowInboxApplicationListAsync(Guid userId)
        {
            return await repository.GetInboxFriendshipListAsync(userId);
        }

        public async Task<IEnumerable<FriendModel>> ShowOutboxApplicationListAsync(Guid userId)
        {
            return await repository.GetOutboxFriendshipListAsync(userId);
        }

        public async Task ApproveApplicationAsync(Guid userId, Guid friendId)
        {
            await repository.ApproveApplicationAsync(userId, friendId);
        }

        public async Task RemoveFriendAsync(Guid userId, Guid friendId)
        {
            await repository.RemoveFriendAsync(userId, friendId);
        }

        public async Task SaveAsync()
        {
            await repository.SaveAsync();
        }
    }
}