using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Domain.ValueObjects;

namespace Refactoring.FraudDetection.Domain.Entities
{
    [TestClass]
    public class OrdersReportTests
    {
        [TestMethod]
        public void ShouldCreateAnInstanceWithFilenameAndOrders()
        {
            // Arrange
            string filename = "filename";
            IEnumerable<Order> orders = [];

            // Act
            OrdersReport ordersReport = new(OrdersReportId.New(filename), orders);

            // Assert
            ordersReport.OrdersReportId.Should().Be(filename);
            ordersReport.Orders.Should().BeEquivalentTo(orders);
        }

        [TestMethod]
        public void ShouldFailsWhenFilenameIsNull()
        {
            // Arrange & Act
            Action action = () => _ = new OrdersReport(null!, []);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("Value cannot be null. (Parameter 'ordersReportId')");
        }

        [TestMethod]
        public void ShouldFailsWhenOrdersAreNull()
        {
            // Arrange & Act
            Action action = () => _ = new OrdersReport(OrdersReportId.New("filename"), null!);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("Value cannot be null. (Parameter 'orders')");
        }

        [TestMethod]
        public void ShouldReturnNoFraudulentOrdersWhenNoOrders()
        {
            // Arrange
            OrdersReport ordersReport = new(OrdersReportId.New("filename"), []);

            // Act
            IEnumerable<Order> fraudulentOrders = ordersReport.GetFraudulentOrders();

            // Assert
            fraudulentOrders.Should().BeEmpty();
        }

        [TestMethod]
        public void ShouldReturnFraudulentOrdersWhenOrdersContainFraudulentOnes()
        {
            // Arrange
            Address address1 = Address.New("New York", State.New("NY"), Street.New("123 Sesame St."), "10011");
            Address address2 = Address.New("Colorado", State.New("CL"), Street.New("1234 Sesame St."), "10011");
            Email email1 = Email.New("bugs@bunny.com");
            Email email2 = Email.New("roger@rabbit.com");
            Order order1 = new(address1, "12345689010", dealId: 1, email1, orderId: 1);
            Order order2 = new(address1, "12345689011", dealId: 1, email1, orderId: 2);
            Order order3 = new(address2, "12345689012", dealId: 2, email2, orderId: 3);
            Order order4 = new(address2, "12345689014", dealId: 2, email2, orderId: 4);
            IEnumerable<Order> orders = [order1, order2, order3, order4];
            OrdersReport ordersReport = new(OrdersReportId.New("filename"), orders);

            // Act
            IEnumerable<Order> result = ordersReport.GetFraudulentOrders();

            // Assert
            result.Should().HaveCount(2);
            result.Should().ContainEquivalentOf(order2);
            result.Should().ContainEquivalentOf(order4);
        }

        [TestMethod]
        public void ShouldReturnEmptyFraudulentOrdersWhenAllAreLegitimate()
        {
            // Arrange
            Address address1 = Address.New(city: "New York", state: State.New("NY"), street: Street.New("123 Sesame St."), zipCode: "10011");
            Address address2 = Address.New(city: "Colorado", state: State.New("CL"), street: Street.New("1234 Sesame St."), zipCode: "10011");
            Address address3 = Address.New(city: "Colorado", state: State.New("CL"), street: Street.New("100 Sesame St."), zipCode: "10011");
            Email email1 = Email.New("bugs@bunny.com");
            Email email2 = Email.New("roger@rabbit.com");
            Email email3 = Email.New("peter@parker.com");
            IEnumerable<Order> orders = [
                new(address1, "12345689010", dealId: 1, email1, orderId: 1),
                new(address1, "12345689010", dealId: 1, email2, orderId: 2),
                new(address2, "12345689011", dealId: 1, email3, orderId: 3),
                new(address3, "12345689011", dealId: 1, email3, orderId: 4),
            ];
            OrdersReport ordersReport = new(OrdersReportId.New("filename"), orders);

            // Act
            IEnumerable<Order> result = ordersReport.GetFraudulentOrders();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
