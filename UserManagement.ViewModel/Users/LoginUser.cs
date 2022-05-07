using Resources;
using Resources.Messages;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.ViewModel.Users
{
    public class LoginUser
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
