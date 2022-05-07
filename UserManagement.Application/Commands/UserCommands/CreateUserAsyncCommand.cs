using MediatR;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.Commands.UserCommands
{
    public record CreateUserAsyncCommand(CreateUser Command) : IRequest<Dtat.Result.Result>;
}
