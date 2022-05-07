using _0_Framework;
using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Infrastructure.EFCore.Repositories;

namespace UserManagement.Application.CommandHandlers.UserCommandHandlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Dtat.Result.Result>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public ChangePasswordCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result();

            var user = await _repository.GetByIdAsync(request.Command.Id);

            if (string.IsNullOrWhiteSpace(request.Command.RepeatNewPassword))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.Password);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }

            if (request.Command.NewPassword != request.Command.RepeatNewPassword)
            {
                var errorMessage = string.Format(Validations.NewPasswordAndRepeatNewPasswordAreNotEqual);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }

            if (user == null)
            {
                var errorMessage = string.Format(Validations.NotFound, DataDictionary.User);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }

            var password = _passwordHasher.Hash(request.Command.NewPassword);

            var changeResult = user.ChangePassword(password);

            if (changeResult.IsFailed)
            {
                result.WithErrors(changeResult.Errors);
                return result.ConvertToDtatResult();
            }

            await _repository.SaveChangesAsync();

            var successMessage = string.Format(Successes.SuccessUpdate, DataDictionary.User);

            result.WithSuccess(successMessage);

            return result.ConvertToDtatResult();
        }
    }
}
