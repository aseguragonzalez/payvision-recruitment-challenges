
namespace Refactoring.FraudDetection.Domain.ValueObjects
{
    public sealed class Email : StringValueObject
    {
        private Email(string value) : base(CleanEmail(value))
        {
        }

        private static string CleanEmail(string email)
        {
            string[] emailBlocks = email.Split(['@'], StringSplitOptions.RemoveEmptyEntries);
            string user = emailBlocks[0].Replace(".", "", StringComparison.InvariantCulture);
            string domain = emailBlocks[1];
            if (user.Contains('+', StringComparison.InvariantCulture))
            {
                user = user.Remove(user.IndexOf('+', StringComparison.InvariantCulture));
            }
            return string.Join("@", new string[] { user, domain });
        }

        public static Email New(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            if (!value.Contains('@', StringComparison.InvariantCulture))
            {
                throw new ArgumentException("The value is not a valid email address.", nameof(value));
            }

            if (value.Count(x => x == '@') > 1)
            {
                throw new ArgumentException("The value is not a valid email address. It contains more than one '@'", nameof(value));
            }

            return new Email(value);
        }
    }
}
