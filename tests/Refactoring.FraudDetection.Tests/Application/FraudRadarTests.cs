using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Refactoring.FraudDetection.Application;
using Refactoring.FraudDetection.Domain;
using Refactoring.FraudDetection.Domain.Entities;
using Refactoring.FraudDetection.Domain.ValueObjects;


namespace Refactoring.FraudDetection.Tests.Application
{
    [TestClass]
    public class FraudRadarTests
    {
        [TestMethod]
        public void ShouldFailsWhenOrdersRepositoryIsMissing()
        {
            // Arrange & Act
            Action act = () => _ = new FraudRadar(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage(
                "Value cannot be null. (Parameter 'ordersReportRepository')"
            );
        }

        [TestMethod]
        public void ShouldReturnEmptyFraudResultsWhenNoOrdersArePresent()
        {
            // Arrange
            OrdersReport ordersReport = new(OrdersReportId.New("filePath"));
            IOrdersReportRepository ordersRepository = Substitute.For<IOrdersReportRepository>();
            ordersRepository.GetOrdersReport(ordersReport.OrdersReportId).Returns(ordersReport);
            FraudRadar fraudRadar = new(ordersRepository);

            // Act
            IEnumerable<FraudResult> result = fraudRadar.Check(ordersReport.OrdersReportId);

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void ShouldReturnEmptyFraudResultsWhenFraudulentOrdersAreEmpty()
        {
            // Arrange
            Order order = new(
                address: Address.New("New York", State.New("NY"), Street.New("123 Sesame St."), "10011"),
                creditCard: "12345689010",
                dealId: 10,
                email: Email.New("bugs@bunny.com"),
                orderId: 1
            );
            OrdersReport ordersReport = new(OrdersReportId.New("filePath"), [order]);
            IOrdersReportRepository ordersRepository = Substitute.For<IOrdersReportRepository>();
            ordersRepository.GetOrdersReport(Arg.Is(ordersReport.OrdersReportId)).Returns(ordersReport);
            FraudRadar fraudRadar = new(ordersRepository);

            // Act
            IEnumerable<FraudResult> result = fraudRadar.Check(ordersReport.OrdersReportId);

            // Assert
            result.Should().HaveCount(0);
            ordersRepository.Received(1).GetOrdersReport(Arg.Is(ordersReport.OrdersReportId));
        }
    }
}
