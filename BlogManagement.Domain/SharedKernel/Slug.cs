using _0_Framework.Application;
using Resources;
using Resources.Messages;

namespace BlogManagement.Domain.SharedKernel
{
    public class Slug
    {
        public const int MinLength = 3;
        public const int MaxLength = 100;

        public string Value { get; }

        private Slug() : base()
        {
        }
        private Slug(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<Slug> Create(string? value)
        {
            var result = new FluentResults.Result<Slug>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.Slug);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length < MinLength)
            {
                var errorMessage = string.Format(Validations.MinLength, DataDictionary.Slug, MinLength);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Slug, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var slug = value.Slugify();

            var returnValue = new Slug(slug);

            result.WithValue(returnValue);

            return result;
        }
    }
}
