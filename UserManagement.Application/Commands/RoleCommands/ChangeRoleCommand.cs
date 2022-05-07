using MediatR;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.Commands.RoleCommands
{
    public record ChangeRoleCommand(ChangeRole ChangeRole) : IRequest<Dtat.Result.Result>;
}
