using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CoreLearning.Infrastructure.Data.Repositories
{
    public sealed class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MessengerContext context) : base(context)
        {
        }

        public async Task<IQueryable<User>> GetUsersAsync(string nickname, string userId)
        {
            return await Task.Run(() => context.Users.Where(user => user.Nickname.Contains(nickname) && user.Id != Guid.Parse(userId)).AsQueryable());
        }

        public async Task<bool> CheckUserIsCreatedAsync(string login, string password)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Login == login && user.Password == password) != null;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Login == login);
        }

        public async Task<User> GetChatsByUserIdAsync(Guid id)
        {
            return await context.Users.Where(user => user.Id.Equals(id)).Select(user => new User {Chats = user.Chats.Select(correspondence => new Correspondence {Id = correspondence.Id}).ToList()}).FirstOrDefaultAsync();
        }

        public async Task UpdateSettings(User user)
        {
            await Task.Run(() => context.Users.Update(user));
        }

        public override async Task<User> GetByIdAsync(Guid id)
        {
            return await context.Users.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }
    }
}