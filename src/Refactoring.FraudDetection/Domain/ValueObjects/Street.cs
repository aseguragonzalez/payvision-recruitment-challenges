namespace Refactoring.FraudDetection.Domain.ValueObjects
{
    public sealed class Street : StringValueObject
    {
        private Street(string street) : base(CleanStreet(street))
        {
        }

        private static string CleanStreet(string street) => street
                .Replace("st.", "street", StringComparison.InvariantCultureIgnoreCase)
                .Replace("rd.", "road", StringComparison.InvariantCultureIgnoreCase)
                .ToUpperInvariant();

        public static Street New(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            return new(value);
        }
    }
}
