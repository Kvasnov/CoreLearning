using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreLearning.Infrastructure.Data
{
    public sealed class UserRepository : IUserRepository
    {
        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        private readonly UserContext context;

        public async Task<List<User>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<bool> CheckUserIsCreatedAsync(string login, string password)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Login == login && user.Password == password) != null;
        }

        public async Task AddUserAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}