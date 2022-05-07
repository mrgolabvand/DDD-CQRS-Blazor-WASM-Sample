using _0_Framework;
using Resources;
using Resources.Messages;

namespace UserManagement.Domain.Aggregates.Users.ValueObjects
{
    public class Password
    {

        const int MinLength = 10;
        const int MaxLength = 1000;
        public const int DbMaxLength = 1000;

        public string Value { get; }

        private Password() : base()
        {
        }
        private Password(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<Password> Create(string value)
        {
            var result = new FluentResults.Result<Password>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.Password);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.Password, MinLength);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Password, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new Password(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
