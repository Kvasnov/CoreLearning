using CoreLearning.DBLibrary.DTO_models;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Commands
{
    public class RegisterCommand : IRequest
    {
        public RegisterCommand(RegisterModel registerModel)
        {
            RegisterModel = registerModel;
        }

        public RegisterModel RegisterModel {get; set;}
    }
}