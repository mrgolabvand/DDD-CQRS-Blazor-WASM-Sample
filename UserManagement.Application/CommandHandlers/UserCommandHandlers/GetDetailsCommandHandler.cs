using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Infrastructure.EFCore.Repositories;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.CommandHandlers.UserCommandHandlers
{
    public class GetDetailsCommandHandler : IRequestHandler<GetDetailsCommand, Dtat.Result.Result<EditUser>>
    {
        private readonly IUserRepository _repository;

        public GetDetailsCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<EditUser>> Handle(GetDetailsCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<EditUser>();

            var isExists = _repository.Exists(v => v.Id == request.Id);
            if (!isExists)
            {
                var errorMessage = string.Format(Validations.NotFound, DataDictionary.User);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }

            var user = _repository.GetDetails(request.Id);

            result.WithValue(user);

            return result.ConvertToDtatResult();
        }
    }
}
