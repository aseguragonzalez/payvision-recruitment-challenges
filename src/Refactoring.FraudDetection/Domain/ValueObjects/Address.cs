namespace Refactoring.FraudDetection.Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string City { get; }

        public State State { get; }

        public Street Street { get; }

        public string ZipCode { get; }

        private Address(string city, State state, Street street, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public static Address New(string city, State state, Street street, string zipCode)
        {
            ArgumentNullException.ThrowIfNull(state, nameof(state));
            ArgumentNullException.ThrowIfNull(street, nameof(street));

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City is invalid or missing.");
            }

            if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentException("ZipCode is invalid or missing.");
            }

            return new(city, state, street, zipCode);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return State;
            yield return Street;
            yield return ZipCode;
        }
    }
}
