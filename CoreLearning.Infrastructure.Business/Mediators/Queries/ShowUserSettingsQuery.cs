using System;
using CoreLearning.DBLibrary.DTO_models;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Queries
{
    public class ShowUserSettingsQuery : IRequest<UserSettingsModel>
    {
        public ShowUserSettingsQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id {get;}
    }
}