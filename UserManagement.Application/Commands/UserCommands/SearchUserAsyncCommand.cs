using MediatR;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.Commands.UserCommands
{
    public record SearchUserAsyncCommand(SearchUserModel SearchModel) : IRequest<Dtat.Result.Result<List<UserViewModel>>>;
}
