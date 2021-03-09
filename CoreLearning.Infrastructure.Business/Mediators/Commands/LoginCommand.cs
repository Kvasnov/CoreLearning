using CoreLearning.DBLibrary.DTO_models;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Commands
{
    public class LoginCommand : IRequest<string>
    {
        public LoginCommand(LoginModel loginModel)
        {
            LoginModel = loginModel;
        }

        public LoginModel LoginModel {get;}
    }
}