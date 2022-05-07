using Resources;
using Resources.Messages;

namespace BlogManagement.Domain.Aggregates.Articles.ValueObjects
{
    public class Description
    {
        public const int MinLength = 10;
        public const int MaxLength = 2000;

        public string Value { get; }

        private Description() : base()
        {
        }
        private Description(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<Description> Create(string? value)
        {
            var result = new FluentResults.Result<Description>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.Description);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.Description);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Description);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new Description(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
