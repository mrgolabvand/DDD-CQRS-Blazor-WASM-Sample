using System.Collections.Generic;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string? Role { get; set; }
        public string? UserName { get; set; }

        public AuthViewModel()
        {
        }

        public AuthViewModel(long id, string userName, long roleId)
        {
            Id = id;
            UserName = userName;
            RoleId = roleId;
        }
    }
}