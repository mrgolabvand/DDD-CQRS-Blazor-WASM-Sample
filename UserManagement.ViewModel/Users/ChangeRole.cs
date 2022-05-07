using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.ViewModel.Users
{
    public class ChangeRole
    {
        public long Id { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }
    }
}
