using Refactoring.FraudDetection.Domain.ValueObjects;

namespace Refactoring.FraudDetection.Domain.Entities
{
    public sealed class OrdersReport
    {
        public OrdersReportId OrdersReportId { get; }

        public IEnumerable<Order> Orders { get; }

        public OrdersReport(OrdersReportId ordersReportId, IEnumerable<Order> orders)
        {
            ArgumentNullException.ThrowIfNull(ordersReportId, nameof(ordersReportId));
            ArgumentNullException.ThrowIfNull(orders, nameof(orders));

            OrdersReportId = ordersReportId;
            Orders = orders;
        }

        public OrdersReport(OrdersReportId ordersReportId) : this(ordersReportId, [])
        {
        }

        public IEnumerable<Order> GetFraudulentOrders() =>
            Orders.SelectMany((current, index) => Orders.Skip(index + 1).Where(order => order.IsFraudulent(current)));
    }
}
