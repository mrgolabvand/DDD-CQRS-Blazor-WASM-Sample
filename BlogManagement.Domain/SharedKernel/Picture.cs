using Resources;
using Resources.Messages;

namespace BlogManagement.Domain.SharedKernel
{
    public class Picture
    {
        public const int MinLength = 3;
        public const int MaxLength = 200;

        public string Value { get; }

        private Picture() : base()
        {
        }
        private Picture(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<Picture> Create(string? value)
        {
            var result = new FluentResults.Result<Picture>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.Picture);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.Picture, MinLength);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Picture, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new Picture(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
