using Resources;
using Resources.Messages;

namespace UserManagement.Domain.SharedKernel
{
    public class FullName
    {
        public const int MinLength = 5;
        public const int MaxLength = 50;

        public string Value { get; }

        private FullName() : base()
        {
        }

        private FullName(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<FullName> Create(string value)
        {
            var result = new FluentResults.Result<FullName>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.FullName);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.FullName, MinLength);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.FullName, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new FullName(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
