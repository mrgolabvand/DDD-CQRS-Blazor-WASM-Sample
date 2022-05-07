using _0_Framework.Infrastructure;
using UserManagement.Domain.Aggregates.Users;
using UserManagement.ViewModel.Users;

namespace UserManagement.Infrastructure.EFCore.Repositories
{
    public interface IUserRepository : IRepository<long, User>
    {
        Task<User?> GetbyUserNameAsync(string userName);
        EditUser? GetDetails(long id);
        ChangeRole? GetChangeRoleDetails(long id);
        Task<List<UserViewModel>> SearchUserAsync(SearchUserModel searchModel);
    }
}
