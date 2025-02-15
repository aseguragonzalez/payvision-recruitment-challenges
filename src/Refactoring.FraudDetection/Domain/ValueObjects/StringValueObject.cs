namespace Refactoring.FraudDetection.Domain.ValueObjects
{
    public abstract class StringValueObject : ValueObject
    {
        public string Value { get; }

        protected StringValueObject(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is string str)
            {
                return Value == str;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => Value;
    }
}
