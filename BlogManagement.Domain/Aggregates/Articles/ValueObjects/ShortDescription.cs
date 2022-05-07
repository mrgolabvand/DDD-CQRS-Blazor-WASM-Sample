using Resources;
using Resources.Messages;

namespace BlogManagement.Domain.Aggregates.Articles.ValueObjects
{
    public class ShortDescription
    {
        public const int MinLength = 10;
        public const int MaxLength = 500;

        public string Value { get; }

        private ShortDescription() : base()
        {
        }
        private ShortDescription(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<ShortDescription> Create(string? value)
        {
            var result = new FluentResults.Result<ShortDescription>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.ShortDescription);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.ShortDescription);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.ShortDescription);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new ShortDescription(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
