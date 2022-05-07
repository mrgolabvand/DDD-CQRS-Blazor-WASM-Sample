using Resources;
using Resources.Messages;

namespace UserManagement.Domain.Aggregates.Users.ValueObjects
{
    public class UserName
    {
        public const int MinLength = 3;
        public const int MaxLength = 20;

        public string Value { get; }

        private UserName() : base()
        {
        }
        private UserName(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<UserName> Create(string value)
        {
            var result = new FluentResults.Result<UserName>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.Username);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.Username, MinLength);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Username, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new UserName(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
