using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Domain.ValueObjects;

namespace Refactoring.FraudDetection.Tests.Domain.ValueObjects
{
    [TestClass]
    public class OrdersReportIdTests
    {
        [TestMethod]
        public void ShouldCreateNewOrdersReportId()
        {
            // Arrange
            string value = "any-value";

            // Act
            var ordersReportId = OrdersReportId.New(value);

            // Assert
            ordersReportId.Should().Be(value);
        }

        [TestMethod]
        public void ShouldFailsWhenValueIsNull()
        {
            // Arrange & Act
            Action act = () => _ = OrdersReportId.New(null!);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'value')");
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        public void ShouldFailsWhenValueIsInvalid(string value)
        {
            // Arrange & Act
            Action act = () => _ = OrdersReportId.New(value);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage(
                "The value cannot be an empty string or composed entirely of whitespace. (Parameter 'value')"
            );
        }
    }
}
