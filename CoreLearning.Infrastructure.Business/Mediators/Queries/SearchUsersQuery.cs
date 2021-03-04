using System;
using System.Collections.Generic;
using CoreLearning.DBLibrary.DTO_models;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Queries
{
    public class SearchUsersQuery : IRequest<List<SearchModel>>
    {
        public SearchUsersQuery(string nickname, Guid id)
        {
            Nickname = nickname;
            Id = id;
        }

        public string Nickname {get;}
        public Guid Id {get;}
    }
}