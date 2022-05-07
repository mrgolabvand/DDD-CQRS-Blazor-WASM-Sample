using Dtat.Result;
using MediatR;
using Resources;
using Resources.Messages;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Infrastructure.EFCore.Repositories;

namespace UserManagement.Application.CommandHandlers.UserCommandHandlers
{
    public class EditAsyncCommandHandler : IRequestHandler<EditAsyncCommand, Dtat.Result.Result>
    {
        private readonly IUserRepository _repository;

        public EditAsyncCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(EditAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result();

            var userToEdit = await _repository.GetByIdAsync(request.Command.Id);
            if (userToEdit == null)
            {
                var errorMessage = string.Format(Validations.NotFound, DataDictionary.User);

                result.WithError(errorMessage);

                return result.ConvertToDtatResult();
            }

            var editResult = userToEdit.Edit(request.Command.FullName, request.Command.UserName, request.Command.PhoneNumber, request.Command.Email);

            if (editResult.IsFailed)
            {
                result.WithErrors(editResult.Errors);
                return result.ConvertToDtatResult();
            }

            await _repository.SaveChangesAsync();

            var successMessage = string.Format(Successes.SuccessUpdate, DataDictionary.User);

            result.WithSuccess(successMessage);

            return result.ConvertToDtatResult();
        }
    }
}
