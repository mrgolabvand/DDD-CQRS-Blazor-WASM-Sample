using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Infrastructure.EFCore.Repositories;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.CommandHandlers.RoleCommandHandlers
{
    public class GetChangeRoleCommandHandler : IRequestHandler<GetChangeRoleCommand, Dtat.Result.Result<ChangeRole>>
    {
        private readonly IUserRepository _repository;

        public GetChangeRoleCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ChangeRole>> Handle(GetChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<ChangeRole>();

            var isExists = _repository.Exists(v => v.Id == request.Id);
            if (!isExists)
            {
                var errorMessage = string.Format(Validations.NotFound, DataDictionary.User);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }

            var user = _repository.GetChangeRoleDetails(request.Id);

            result.WithValue(user);

            return result.ConvertToDtatResult();
        }
    }
}
