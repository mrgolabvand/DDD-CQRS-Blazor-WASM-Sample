using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Infrastructure.EFCore.Repositories;

namespace UserManagement.Application.CommandHandlers.RoleCommandHandlers
{
    public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand, Dtat.Result.Result>
    {
        private readonly IUserRepository _repository;

        public ChangeRoleCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result();

            var user = await _repository.GetByIdAsync(request.ChangeRole.Id);
            
            if (user == null)
            {
                var errorMessage = string.Format(Validations.NotFound, DataDictionary.User);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }

            var changeResult = user.ChangeRole(request.ChangeRole.RoleId);

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
