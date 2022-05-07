using UserManagement.Domain.Aggregates.Users.ValueObjects;
using UserManagement.Domain.SharedKernel;

namespace UserManagement.Domain.Aggregates.Users
{
    public class User
    {
        public long Id { get; private set; }
        public FullName FullName { get; private set; }
        public UserName UserName { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public virtual Role Role { get; private set; }

        private User() : base()
        {
        }

        public User(FullName fullName, UserName userName, PhoneNumber phoneNumber, Email email, Password password, Role role) : this()
        {
            FullName = fullName;
            UserName = userName;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            Role = role;
        }

        public static FluentResults.Result<User> Create(string? fullName, string? userName, string? phoneNumber, string? email, string? password, int? role)
        {
            var result = new FluentResults.Result<User>();

            var fullNameResult = FullName.Create(fullName);
            result.WithErrors(fullNameResult.Errors);

            var userNameResult = UserName.Create(userName);
            result.WithErrors(userNameResult.Errors);

            var phoneNumberResult = PhoneNumber.Create(phoneNumber);
            result.WithErrors(phoneNumberResult.Errors);

            var emailResult = Email.Create(email);
            result.WithErrors(emailResult.Errors);

            var passwordResult = Password.Create(password);
            result.WithErrors(passwordResult.Errors);

            var roleResult = Role.GetByValue(role);
            result.WithErrors(roleResult.Errors);

            if (result.IsFailed)
                return result;

            var returnValue = new User(fullNameResult.Value, userNameResult.Value, phoneNumberResult.Value, emailResult.Value, passwordResult.Value, roleResult.Value);

            result.WithValue(returnValue);

            return result;
        }

        public FluentResults.Result Edit(string? fullName, string? userName, string? phoneNumber, string? email)
        {
            var result = Create(fullName, userName, phoneNumber, email, Password.Value, 0);

            if(result.IsFailed)
                return result.ToResult();

            FullName = result.Value.FullName;
            UserName = result.Value.UserName;
            PhoneNumber = result.Value.PhoneNumber;
            Email = result.Value.Email;
            Password = result.Value.Password;
            Role = result.Value.Role;

            return result.ToResult();

        }
        public FluentResults.Result ChangePassword(string? password)
        {
            var result = new FluentResults.Result();

            var newPasswordResult = Password.Create(value: password);

            if (newPasswordResult.IsFailed)
            {
                result.WithErrors(errors: newPasswordResult.Errors);

                return result;
            }

            Password =
                newPasswordResult.Value;

            return result;
        }
        public FluentResults.Result ChangeRole(int? roleId)
        {
            var result = Role.GetByValue(roleId);

            if (result.IsFailed)
                return result.ToResult();
            
            Role = result.Value;

            return result.ToResult();
        }
    }
}
