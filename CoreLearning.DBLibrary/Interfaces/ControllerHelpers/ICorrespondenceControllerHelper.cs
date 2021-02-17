using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;

namespace CoreLearning.DBLibrary.Interfaces.ControllerHelpers
{
    public interface ICorrespondenceControllerHelper
    {
        Task<string> FindChatAsync(string senderId, Guid receiverId);
        Task<string> CreateChatAsync(string senderId, Guid receiverId);
        Task SaveAsync();
        Task SendMessageAsync(string correspondenceId, string senderId, string description);
        Task<ChatModel> ShowChatAsync(string correspondenceId);
        Task<IEnumerable<Tuple<Guid, string>>> ShowAllChatsAsync(Guid senderId);
    }
}