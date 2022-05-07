using Resources;
using Resources.Messages;

namespace BlogManagement.Domain.SharedKernel
{
    public class Title
    {
        public const int MinLength = 3;
        public const int MaxLength = 60;

        public string Value { get; }

        private Title() : base()
        {
        }
        private Title(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<Title> Create(string? value)
        {
            var result = new FluentResults.Result<Title>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.Title);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.Title, MinLength);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Title, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new Title(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
