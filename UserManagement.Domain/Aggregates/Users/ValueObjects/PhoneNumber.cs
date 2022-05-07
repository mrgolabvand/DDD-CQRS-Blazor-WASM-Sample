using Resources;
using Resources.Messages;

namespace UserManagement.Domain.Aggregates.Users.ValueObjects
{
    public class PhoneNumber
    {
        public const int FixLength = 11;

        public string Value { get; }

        private PhoneNumber() : base()
        {
        }
        private PhoneNumber(string value) : this()
        {
            Value = value;
        }

        public static FluentResults.Result<PhoneNumber> Create(string value)
        {
            var result = new FluentResults.Result<PhoneNumber>();

            if (string.IsNullOrWhiteSpace(value))
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.CellPhoneNumber);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length != FixLength)
            {
                var errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.CellPhoneNumber, FixLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new PhoneNumber(value);

            result.WithValue(returnValue);

            return result;
        }
    }
}
