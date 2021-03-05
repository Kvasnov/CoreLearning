using System;
using System.Threading;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Entities;
using CoreLearning.DBLibrary.Interfaces.Repositories;
using CoreLearning.Infrastructure.Business.Mediators.Commands;
using MediatR;

namespace CoreLearning.Infrastructure.Business.Mediators.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand>
    {
        public RegisterHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        private readonly IUserRepository repository;

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await repository.CheckUserIsCreatedAsync(request.RegisterModel.Email, request.RegisterModel.Password))
                throw new Exception("Такой пользователь уже сеществует");

            var user = new User
                       {
                           Login = request.RegisterModel.Email,
                           Password = request.RegisterModel.Password,
                           Name = request.RegisterModel.Name,
                           LastName = request.RegisterModel.LastName,
                           Nickname = request.RegisterModel.Nickname
                       };

            await repository.AddAsync(user);
            await repository.SaveAsync();

            return Unit.Value;
        }
    }
}