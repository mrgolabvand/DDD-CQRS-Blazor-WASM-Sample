using MediatR;
using UserManagement.ViewModel.Users;

namespace UserManagement.Application.Queries
{
    public record SearchAsyncQuery(string? query) : IRequest<Dtat.Result.Result<List<UserViewModel>>>;
}
