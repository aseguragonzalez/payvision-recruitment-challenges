namespace Refactoring.FraudDetection.Infrastructure
{
    using Refactoring.FraudDetection.Domain.ValueObjects;

    internal static class OrderExtensions
    {
#pragma warning disable CS8604 // Possible null reference argument. Ignore because extending class with `this` reference argument.
        public static Domain.Entities.Order ToEntity(this Order order) =>
            new(
                address: Address.New(order.City, State.New(order.State), Street.New(order.Street), order.ZipCode),
                creditCard: order.CreditCard,
                dealId: order.DealId,
                email: Email.New(order.Email),
                orderId: order.OrderId
            );
#pragma warning restore CS8604 // Possible null reference argument.
    }
}
