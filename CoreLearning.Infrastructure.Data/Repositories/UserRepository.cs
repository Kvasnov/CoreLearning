using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces;
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
            return await Task.Run(()=> context.Users.Where(user => user.Nickname.Contains(nickname) && user.Id != new Guid(userId) ).AsQueryable());
        }

        public async Task<bool> CheckUserIsCreatedAsync(string login, string password)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Login == login && user.Password == password) != null;
        }

        public async Task<User> GetkUserAsync(string login)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Login == login);
        }
    }
}