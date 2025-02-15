using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Domain.Entities;
using Refactoring.FraudDetection.Domain.ValueObjects;
using Refactoring.FraudDetection.Infrastructure;

namespace Refactoring.FraudDetection.Tests.Infrastructure
{
    [TestClass]
    public class OrdersRepositoryTests
    {
        [TestMethod]
        public void ShouldFailsWhenOrdersReportIdIsNull()
        {
            // Arrange
            OrdersRepository ordersRepository = new();

            // Act
            Action act = () => ordersRepository.GetOrdersReport(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage(
                "Value cannot be null. (Parameter 'ordersReportId')"
            );
        }

        [TestMethod]
        public void ShouldReturnsAllOrders()
        {
            // Arrange
            OrdersRepository ordersRepository = new();

            // Act
            OrdersReport ordersReport = ordersRepository.GetOrdersReport(
                OrdersReportId.New("ThreeLines_FraudulentSecond.txt")
            );

            // Assert
            ordersReport.Orders.Count().Should().Be(3);
        }
    }
}
