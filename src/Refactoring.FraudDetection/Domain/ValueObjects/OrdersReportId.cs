namespace Refactoring.FraudDetection.Domain.ValueObjects
{
    public sealed class OrdersReportId : StringValueObject
    {
        private OrdersReportId(string value) : base(value)
        {
        }

        public static OrdersReportId New(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            return new OrdersReportId(value);
        }

        public override string ToString() => Value;
    }
}
