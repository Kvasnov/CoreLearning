using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CoreLearning.Infrastructure.Data.Repositories
{
    public class FriendshipRepository : BaseRepository<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(MessengerContext context) : base(context)
        {
        }

        public async Task<IQueryable<FriendModel>> GetFriendListAsync(Guid userId)
        {
            return await Task.Run(() => context.Friendships.Where(friend => friend.UserId.Equals(userId) && friend.IsFriend).
                                                Select(friend => new FriendModel {Id = friend.FriendId, Name = friend.UserFriend.Name, LastName = friend.UserFriend.LastName, Nickname = friend.UserFriend.Nickname}));
        }

        public async Task<IQueryable<FriendModel>> GetInboxFriendshipListAsync(Guid userId)
        {
            return await Task.Run(() => context.Friendships.Where(friend => friend.UserId.Equals(userId) && friend.Inbox && !friend.IsFriend).
                                                Select(friend => new FriendModel {Id = friend.FriendId, Name = friend.UserFriend.Name, LastName = friend.UserFriend.LastName, Nickname = friend.UserFriend.Nickname}));
        }

        public async Task<IQueryable<FriendModel>> GetOutboxFriendshipListAsync(Guid userId)
        {
            return await Task.Run(() => context.Friendships.Where(friend => friend.UserId.Equals(userId) && friend.Inbox == false && !friend.IsFriend).
                                                Select(friend => new FriendModel {Id = friend.FriendId, Name = friend.UserFriend.Name, LastName = friend.UserFriend.LastName, Nickname = friend.UserFriend.Nickname}));
        }

        public async Task ApproveApplicationAsync(Guid userId, Guid friendId)
        {
            var userFriend = await context.Friendships.FirstOrDefaultAsync(friend => friend.FriendId.Equals(friendId));
            var friendUser = await context.Friendships.FirstOrDefaultAsync(friend => friend.FriendId.Equals(userId));
            userFriend.IsFriend = true;
            friendUser.IsFriend = true;
        }

        public async Task RemoveFriendAsync(Guid userId, Guid friendId)
        {
            var userFriend = await context.Friendships.FirstOrDefaultAsync(friend => friend.FriendId.Equals(friendId));
            var friendUser = await context.Friendships.FirstOrDefaultAsync(friend => friend.FriendId.Equals(userId));
            context.Friendships.Remove(userFriend);
            context.Friendships.Remove(friendUser);
        }
    }
}