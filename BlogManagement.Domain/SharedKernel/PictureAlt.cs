using Resources;
using Resources.Messages;

namespace BlogManagement.Domain.SharedKernel
{
    public class PictureAlt
    {
        public const int MinLength = 3;
        public const int MaxLength = 70;

        public string Value { get; }

        private PictureAlt() : base()
        {
        }
        private PictureAlt(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<PictureAlt> Create(string? value)
        {
            var result = new FluentResults.Result<PictureAlt>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.PictureAlt);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.PictureAlt, MinLength);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.PictureAlt, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new PictureAlt(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
