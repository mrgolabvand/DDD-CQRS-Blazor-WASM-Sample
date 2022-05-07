using _0_Framework;
using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Domain.Aggregates.Users;
using UserManagement.Domain.Aggregates.Users.ValueObjects;
using UserManagement.Infrastructure.EFCore.Repositories;

namespace UserManagement.Application.CommandHandlers.UserCommandHandlers
{
    public class CreateUserAsyncCommandHandler : IRequestHandler<CreateUserAsyncCommand, Dtat.Result.Result>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserAsyncCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> Handle(CreateUserAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result();

            var role = Role.User.Value;

            var password = _passwordHasher.Hash(request.Command.Password);


            var user = User.Create(request.Command.FullName, request.Command.UserName, request.Command.PhoneNumber, request.Command.Email, password, role);

            result.WithErrors(user.Errors);

            if (user.IsFailed)
                return result.ConvertToDtatResult();

            await _repository.CreateAsync(user.Value);

            var successMessage = string.Format(Successes.SuccessCreate, DataDictionary.User);

            result.WithSuccess(successMessage);

            return result.ConvertToDtatResult();
        }
    }
}
