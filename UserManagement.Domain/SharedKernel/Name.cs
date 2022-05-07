using Resources;
using Resources.Messages;

namespace UserManagement.Domain.SharedKernel
{
    public class Name
    {
        public const int MinLength = 3;
        public const int MaxLength = 50;

        public string Value { get; }

        private Name() : base()
        {
        }

        private Name(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<Name> Create(string value)
        {
            var result = new FluentResults.Result<Name>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.Name);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.Name, MinLength);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Name, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new Name(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
