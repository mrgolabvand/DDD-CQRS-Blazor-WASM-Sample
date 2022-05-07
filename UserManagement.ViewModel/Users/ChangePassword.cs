namespace UserManagement.ViewModel.Users
{
    public class ChangePassword
    {
        public long Id { get; set; }
        public string? NewPassword { get; set; }
        public string? RepeatNewPassword { get; set; }
    }
}
