using System;
using CoreLearning.DBLibrary.DTO_models;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Commands
{
    public class ChangeUserSettingsCommand : IRequest<UserSettingsModel>
    {
        public ChangeUserSettingsCommand(Guid id, ChangeUserSettingsModel newSettings)
        {
            Id = id;
            Settings = newSettings;
        }

        public ChangeUserSettingsModel Settings {get;}
        public Guid Id {get;}
    }
}