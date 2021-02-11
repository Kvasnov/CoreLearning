using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;

namespace CoreLearning.DBLibrary.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync();
        Task AddUserAsync(User user);
        Task SaveAsync();
        Task<bool> CheckUserIsCreatedAsync(string login, string password);
    }
}