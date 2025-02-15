namespace Refactoring.FraudDetection.Domain.ValueObjects
{
    public sealed class State : StringValueObject
    {
        private State(string value) : base(CleanState(value))
        {
        }

        private static string CleanState(string value) => value
                .Replace("il", "illinois", StringComparison.InvariantCultureIgnoreCase)
                .Replace("ca", "california", StringComparison.InvariantCultureIgnoreCase)
                .Replace("ny", "new york", StringComparison.InvariantCultureIgnoreCase)
                .ToUpperInvariant();

        public static State New(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            return new(value);
        }
    }
}
