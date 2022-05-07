using System.Linq.Expressions;
using UserManagement.ViewModel.Users;

namespace UserManagement.Infrastructure.EFCore.Repositories
{
    public interface IUserQueryRepository
    {
        UserViewModel? GetById(long id);
        UserViewModel? GetByName(string? name);
        Task<List<UserViewModel>> SearchAsync(string? search);

    }
}
