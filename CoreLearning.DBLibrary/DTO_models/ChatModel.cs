using System.Collections.Generic;

namespace CoreLearning.DBLibrary.DTO_models
{
    public class ChatModel
    {
        public ChatModel()
        {
            MessageHistory = new List<MessageModel>();
        }

        public List<MessageModel> MessageHistory {get; set;}
        public string Name {get; set;}
    }
}