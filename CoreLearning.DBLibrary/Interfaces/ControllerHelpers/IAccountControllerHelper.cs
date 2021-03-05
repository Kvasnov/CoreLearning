using System;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;

namespace CoreLearning.DBLibrary.Interfaces.ControllerHelpers
{
    public interface IAccountControllerHelper
    {
        Task AddUserAsync(RegisterModel registerModel);
        Guid GetUserId(string login);
        Task<bool> CheckUserIsCreated(string login, string password);
        Task SaveAsync();
    }
}