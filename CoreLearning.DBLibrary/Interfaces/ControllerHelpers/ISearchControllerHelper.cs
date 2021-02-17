using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;

namespace CoreLearning.DBLibrary.Interfaces.ControllerHelpers
{
    public interface ISearchControllerHelper
    {
        Task<IEnumerable<SearchModel>> SearchUsersAsync(string nickname, string userId);
    }
}