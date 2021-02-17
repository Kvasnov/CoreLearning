using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;

namespace CoreLearning.DBLibrary.Interfaces.ControllerHelpers
{
    public interface IUserSettingsControllerHelper
    {
        Task<UserSettingsModel> GetUserSettingsAsync(string userId);
        Task<UserSettingsModel> SetUserSettingsAsync(string userId, ChangeUserSettingsModel newSettings);
        Task SaveAsync();
    }
}