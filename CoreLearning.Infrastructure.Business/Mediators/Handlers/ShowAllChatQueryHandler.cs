using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Queries;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class ShowAllChatQueryHandler : IRequestHandler<ShowAllChatQuery, IEnumerable<Tuple<Guid, string>>>
    {
        public ShowAllChatQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private readonly IUserRepository userRepository;

        public async Task<IEnumerable<Tuple<Guid, string>>> Handle(ShowAllChatQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetChatsByUserIdAsync(request.UserId);

            return user.Chats.Select(correspondence => new Tuple<Guid, string>(correspondence.Id, correspondence.Name));
        }
    }
}