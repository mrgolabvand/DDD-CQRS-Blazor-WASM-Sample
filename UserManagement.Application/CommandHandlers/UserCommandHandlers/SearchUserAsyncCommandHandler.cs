using Dtat.Result;
using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Infrastructure.EFCore.Repositories;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.CommandHandlers.UserCommandHandlers
{
    public class SearchUserAsyncCommandHandler : IRequestHandler<SearchUserAsyncCommand, Dtat.Result.Result<List<UserViewModel>>>
    {
        private readonly IUserRepository _repository;

        public SearchUserAsyncCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<UserViewModel>>> Handle(SearchUserAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<List<UserViewModel>>();

            var searchResult = await _repository.SearchUserAsync(request.SearchModel);

            result.WithValue(searchResult);

            return result.ConvertToDtatResult();
        }
    }
}
