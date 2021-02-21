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
    public class FriendshipControllerHelper : IFriendshipControllerHelper
    {
        public FriendshipControllerHelper(IUserRepository repository)
        {
            this.repository = repository;
        }

        private readonly IUserRepository repository;

        public async Task AddToFriendsAsync(Guid userId, Guid friendId)
        {
            var user = await repository.GetByIdAsync(userId);
            var friend = await repository.GetByIdAsync(friendId);
            user.Friends.Add(new Friendship {UserId = userId, FriendId = friendId});
            friend.Friends.Add(new Friendship {UserId = friendId, FriendId = userId, Inbox = true});
        }

        public async Task<List<FriendModel>> ShowFriedListAsync(Guid userId)
        {
            var user = await repository.GetByIdAsync(userId);

            return user.Friends.Where(friend => friend.IsFriend).
                        Select(friend => new FriendModel {Id = friend.UserId, Name = friend.UserFriend.Name, LastName = friend.UserFriend.LastName, Nickname = friend.UserFriend.Nickname}).
                        ToList();
        }

        public async Task<List<FriendModel>> ShowInboxApplicationListAsync(Guid userId)
        {
            var user = await repository.GetFriendsByUserIdAsync(userId);
            var test = user.Friends.Where(friend => friend.Inbox).
                            Select(friend => new FriendModel { Id = friend.UserId, Name = friend.UserFriend.Name, LastName = friend.UserFriend.LastName, Nickname = friend.UserFriend.Nickname }).
                            ToList();
            return test;
        }

        public async Task<List<FriendModel>> ShowOutboxApplicationListAsync(Guid userId)
        {
            var user = await repository.GetByIdAsync(userId);

            return user.Friends.Where(friend => friend.Inbox == false).
                        Select(friend => new FriendModel {Id = friend.UserId, Name = friend.UserFriend.Name, LastName = friend.UserFriend.LastName, Nickname = friend.UserFriend.Nickname}).
                        ToList();
        }

        public async Task ApproveApplicationAsync(Guid userId, Guid friendId)
        {
            var user = await repository.GetByIdAsync(userId);
            var friend = await repository.GetByIdAsync(friendId);
            user.Friends.First(frnd => frnd.FriendId.Equals(friendId)).IsFriend = true;
            friend.Friends.First(frnd => frnd.FriendId.Equals(userId)).IsFriend = true;
        }

        public async Task RemoveFriendAsync(Guid userId, Guid friendId)
        {
            var user = await repository.GetByIdAsync(userId);
            var friend = await repository.GetByIdAsync(friendId);
            user.Friends.Remove(user.Friends.First(frnd => frnd.FriendId.Equals(friendId)));
            friend.Friends.Remove(friend.Friends.First(frnd => frnd.FriendId.Equals(userId)));
        }

        public async Task SaveAsync()
        {
            await repository.SaveAsync();
        }
    }
}