using System;
using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Interfaces;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Commands;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        public LoginCommandHandler(ITokenService tokenService, IUserRepository repository)
        {
            this.tokenService = tokenService;
            this.repository = repository;
        }

        private readonly IUserRepository repository;
        private readonly ITokenService tokenService;

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!await repository.CheckUserIsCreatedAsync(request.LoginModel.Email, request.LoginModel.Password))
                throw new Exception("Неверный логин или пароль");

            var user = await repository.GetUserByLoginAsync(request.LoginModel.Email);

            return tokenService.CreateToken(request.LoginModel.Email, user.Id);
        }
    }
}