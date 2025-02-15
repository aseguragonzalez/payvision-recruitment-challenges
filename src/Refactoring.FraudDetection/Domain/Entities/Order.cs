using Refactoring.FraudDetection.Domain.ValueObjects;

namespace Refactoring.FraudDetection.Domain.Entities
{
    public sealed class Order
    {
        public Address Address { get; }

        public string CreditCard { get; }

        public int DealId { get; }

        public Email Email { get; }

        public int OrderId { get; }

        public Order(Address address, string creditCard, int dealId, Email email, int orderId)
        {
            ArgumentNullException.ThrowIfNull(address, nameof(address));
            ArgumentException.ThrowIfNullOrWhiteSpace(creditCard, nameof(creditCard));
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(dealId, 0, nameof(dealId));
            ArgumentNullException.ThrowIfNull(email, nameof(email));
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(orderId, 0, nameof(orderId));
            OrderId = orderId;
            DealId = dealId;
            Email = email;
            Address = address;
            CreditCard = creditCard;
        }

        public bool IsFraudulent(Order order)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));

            return HasDifferentCreditCardAndSameEmailAndDealId(order)
                || HasDifferentCreditCardAndSameAddressAndDealId(order);
        }

        private bool HasDifferentCreditCardAndSameEmailAndDealId(Order order) =>
            DealId == order.DealId && Email == order.Email && CreditCard != order.CreditCard;

        private bool HasDifferentCreditCardAndSameAddressAndDealId(Order order) =>
            DealId == order.DealId && Address == order.Address && CreditCard != order.CreditCard;
    }
}
