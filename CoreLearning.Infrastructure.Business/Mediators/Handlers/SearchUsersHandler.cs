using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Queries;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class SearchUsersHandler : IRequestHandler<SearchUsersQuery, List<SearchModel>>
    {
        public SearchUsersHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        private readonly IUserRepository repository;

        public async Task<List<SearchModel>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await repository.GetUsersAsync(request.Nickname, request.Id);

            return users.Select(user => new SearchModel {Id = user.Id, Name = user.Name, LastName = user.LastName, Nickname = user.Nickname}).ToList();
        }
    }
}