using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserManagement.ViewModel.Users;

namespace UserManagement.Infrastructure.EFCore.Repositories
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly UserContext _context;

        public UserQueryRepository(UserContext context)
        {
            _context = context;
        }


        public UserViewModel? GetById(long id)
        {
            return _context.Users.Select(v => new UserViewModel
            {
                Id = v.Id,
                Email = v.Email.Value,
                FullName = v.FullName.Value,
                PhoneNumber = v.PhoneNumber.Value,
                RoleId = v.Role.Value,
                UserName = v.UserName.Value
            }).FirstOrDefault(v => v.Id == id);
        }

        public UserViewModel? GetByName(string? name)
        {
            return _context.Users.Select(v => new UserViewModel
            {
                Id = v.Id,
                Email = v.Email.Value,
                FullName = v.FullName.Value,
                PhoneNumber = v.PhoneNumber.Value,
                RoleId = v.Role.Value,
                UserName = v.UserName.Value
            }).FirstOrDefault(v => v.UserName == name);
        }

        public async Task<List<UserViewModel>> SearchAsync(string? search)
        {
            var users = await _context.Users.ToListAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                users = users.Where(v => v.FullName.Value.Contains(search) 
                || v.UserName.Value.Contains(search)
                || v.Email.Value.Contains(search)
                || v.PhoneNumber.Value.Contains(search)).ToList();
            }

            return users.Select(v => new UserViewModel
            {
                Id = v.Id,
                FullName = v.FullName.Value,
                PhoneNumber = v.PhoneNumber.Value,
                Email = v.Email.Value,
                UserName = v.UserName.Value,
            }).ToList();
        }
    }
}
