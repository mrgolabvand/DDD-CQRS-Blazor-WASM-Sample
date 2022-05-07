using MediatR;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.Commands.UserCommands
{
    public record EditAsyncCommand(EditUser Command) : IRequest<Dtat.Result.Result>;
}
