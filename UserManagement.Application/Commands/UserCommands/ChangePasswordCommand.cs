using MediatR;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.Commands.UserCommands
{
    public record ChangePasswordCommand(ChangePassword Command) : IRequest<Dtat.Result.Result>; 
}
