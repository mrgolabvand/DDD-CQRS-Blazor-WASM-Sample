using MediatR;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.Commands.UserCommands
{
    public record GetDetailsCommand(long Id) : IRequest<Dtat.Result.Result<EditUser>>;
}
