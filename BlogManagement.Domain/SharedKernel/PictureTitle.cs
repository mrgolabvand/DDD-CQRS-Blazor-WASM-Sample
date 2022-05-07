using Resources;
using Resources.Messages;

namespace BlogManagement.Domain.SharedKernel
{
    public class PictureTitle
    {
        public const int MinLength = 3;
        public const int MaxLength = 50;

        public string Value { get; }

        private PictureTitle() : base()
        {
        }
        private PictureTitle(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<PictureTitle> Create(string? value)
        {
            var result = new FluentResults.Result<PictureTitle>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.PictureTitle);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.PictureTitle, MinLength);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.PictureTitle, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new PictureTitle(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
