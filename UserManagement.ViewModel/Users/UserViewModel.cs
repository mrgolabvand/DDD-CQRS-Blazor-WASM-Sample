namespace UserManagement.ViewModel.Users
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }
    }
}
