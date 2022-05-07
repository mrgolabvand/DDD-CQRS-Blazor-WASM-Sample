using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserManagement.Domain.Aggregates.Users;
using UserManagement.Domain.Aggregates.Users.ValueObjects;
using UserManagement.ViewModel.Users;

namespace UserManagement.Infrastructure.EFCore.Repositories
{
    public class UserRepository : RepositoryBase<long, User> , IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetbyUserNameAsync(string userName)
        {
            var users = await _context.Users.Include(v => v.Role).ToListAsync();
            return users.FirstOrDefault(v => v.UserName.Value == userName);
        }

        public ChangeRole? GetChangeRoleDetails(long id)
        {
            return _context.Users.Include(v => v.Role).Select(v => new ChangeRole
            {
                Id = v.Id,
                RoleId = v.Role.Value,
                Role = v.Role.Name
            }).FirstOrDefault(v => v.Id == id);
        }

        public EditUser? GetDetails(long id)
        {
            return _context.Users.Select(v => new EditUser
            {
                Id = v.Id,
                FullName = v.FullName.Value,
                Email = v.Email.Value,
                PhoneNumber = v.PhoneNumber.Value,
                UserName = v.UserName.Value,
            }).FirstOrDefault(v => v.Id == id);
        }

        public async Task<List<UserViewModel>> SearchUserAsync(SearchUserModel searchModel)
        {
            var users = await _context.Users.Include(v => v.Role).ToListAsync();

            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                    users = users.Where(v => v.FullName.Value.Contains(searchModel.FullName)).ToList();
                
                if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                    users = users.Where(v => v.UserName.Value.Contains(searchModel.UserName)).ToList();

                if (!string.IsNullOrWhiteSpace(searchModel.Email))
                    users = users.Where(v => v.Email.Value.Contains(searchModel.Email)).ToList();
                
                if (!string.IsNullOrWhiteSpace(searchModel.PhoneNumber))
                    users = users.Where(v => v.PhoneNumber.Value.Contains(searchModel.PhoneNumber)).ToList();
            }

            return users.Select(v => new UserViewModel
            {
                Id = v.Id,
                FullName = v.FullName.Value,
                PhoneNumber = v.PhoneNumber.Value,
                Email = v.Email.Value,
                UserName = v.UserName.Value,
                RoleId = v.Role.Value,
                Role = v.Role.Name
            }).ToList();
        }
    }
}
