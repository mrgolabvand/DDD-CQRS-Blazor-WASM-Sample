using MediatR;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.Commands.RoleCommands
{
    public record GetChangeRoleCommand(long Id) : IRequest<Dtat.Result.Result<ChangeRole>>;
}
