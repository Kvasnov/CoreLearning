using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CoreLearning.Infrastructure.Data.Repositories
{
    public sealed class CorrespondenceRepository : BaseRepository<Correspondence>, ICorrespondenceRepository
    {
        public CorrespondenceRepository(MessengerContext context) : base(context)
        {
        }

        public async Task<string> FindChatAsync(string senderId, Guid receiverId)
        {
            var correspondence = await context.Correspondences.Where(ch => ch.Users.Any(user => user.Id.ToString() == senderId)).FirstOrDefaultAsync(ch => ch.Users.Any(user => user.Id.Equals(receiverId)));

            return correspondence?.Id.ToString();
        }

        public async Task AddMessageAsync(string correspondenceId, Message message)
        {
            await Task.CompletedTask;
            context.Correspondences.FirstOrDefault(correspondence => correspondence.Id.ToString() == correspondenceId)?.MessageHistory.Add(message);
        }

        public async Task<Correspondence> GetByIdAsync(Guid id)
        {
            return await context.Correspondences.Where(corr => corr.Id.Equals(id)).
                                 Select(corr => new Correspondence
                                                {
                                                    Name = corr.Name, MessageHistory = corr.MessageHistory.Select(message => new Message {Description = message.Description, SenderUserId = message.SenderUserId}).ToList()
                                                }).
                                 FirstOrDefaultAsync();
        }
    }
}