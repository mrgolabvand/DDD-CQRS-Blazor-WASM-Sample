using Resources;
using Resources.Messages;

namespace UserManagement.Domain.Aggregates.Users.ValueObjects
{
    public class Email
    {
        public const int MinLength = 8;
        public const int MaxLength = 70;

        public string Value { get; }

        private Email() : base()
        {
        }
        private Email(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<Email> Create(string value)
        {
            var result = new FluentResults.Result<Email>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.EmailAddress);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.EmailAddress, MinLength);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.EmailAddress, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new Email(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
