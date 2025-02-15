using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Domain.ValueObjects;
using Refactoring.FraudDetection.Domain.Entities;

namespace Refactoring.FraudDetection.Tests.Domain.Entities
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void ShouldCreateAnInstanceOfOrder()
        {
            // Arrange and Act
            Address address = Address.New("city", State.New("state"), Street.New("street"), "zipCode");
            Email email = Email.New("email@domain.com");
            Order order = new(address, creditCard: "creditCard", dealId: 1, email, orderId: 1);

            // Assert
            order.Should().NotBeNull();
            order.Address.Should().Be(address);
            order.CreditCard.Should().Be("creditCard");
            order.DealId.Should().Be(1);
            order.Email.Should().Be(email);
            order.OrderId.Should().Be(1);
        }

        [TestMethod]
        public void ShouldFailsWhenAddressIsMissing()
        {
            // Arrange and Act
            Email email = Email.New("email@domain.com");
            Action act = () => _ = new Order(null!, creditCard: "creditCard", dealId: 1, email, orderId: 1);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'address')");
        }

        [TestMethod]
        public void ShouldFailsWhenEmailIsMissing()
        {
            // Arrange and Act
            Address address = Address.New("city", State.New("state"), Street.New("street"), "zipCode");
            Action act = () => _ = new Order(address, creditCard: "creditCard", dealId: 1, null!, orderId: 1);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'email')");
        }

        [TestMethod]
        public void ShouldFailsWhenCreditCardIsMissing()
        {
            // Arrange and Act
            Address address = Address.New("city", State.New("state"), Street.New("street"), "zipCode");
            Email email = Email.New("email@domain.com");
            Action act = () => _ = new Order(address, creditCard: null!, dealId: 1, email, orderId: 1);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Value cannot be null. (Parameter 'creditCard')");
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        public void ShouldFailsWhenCreditCardIsMissing(string value)
        {
            // Arrange and Act
            Address address = Address.New("city", State.New("state"), Street.New("street"), "zipCode");
            Email email = Email.New("email@domain.com");
            Action act = () => _ = new Order(address, creditCard: value, dealId: 1, email, orderId: 1);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Value cannot be null. (Parameter 'creditCard')");
        }

        [TestMethod]
        public void ShouldFailsWhenDealIdIsInvalid()
        {
            // Arrange and Act
            Address address = Address.New("city", State.New("state"), Street.New("street"), "zipCode");
            Email email = Email.New("email@domain.com");
            Action act = () => _ = new Order(address, creditCard: "creditCard", dealId: 0, email, orderId: 1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("dealId ('0') must be greater than '0'. (Parameter 'dealId')\nActual value was 0.");
        }

        [TestMethod]
        public void ShouldFailsWhenOrderIdIsInvalid()
        {
            // Arrange and Act
            Address address = Address.New("city", State.New("state"), Street.New("street"), "zipCode");
            Email email = Email.New("email@domain.com");
            Action act = () => _ = new Order(address, creditCard: "creditCard", dealId: 1, email, orderId: 0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("orderId ('0') must be greater than '0'. (Parameter 'orderId')\nActual value was 0.");
        }

        [TestMethod]
        public void ShouldBeFraudulentWithSameEmailAndDealId()
        {
            // Arrange
            Address address1 = Address.New("city", State.New("state"), Street.New("street"), "zipCode");
            Address address2 = Address.New("city", State.New("state"), Street.New("street"), "zipCode");
            Email email = Email.New("email@domain.com");
            Order order = new(address1, creditCard: "creditCard", dealId: 1, email, orderId: 1);
            Order anotherOrder = new(address2, creditCard: "anotherCreditCard", dealId: 1, email, orderId: 2);

            // Act
            bool isFraudulent = order.IsFraudulent(anotherOrder);

            // Assert
            isFraudulent.Should().BeTrue();
        }

        [TestMethod]

        public void ShouldBeFraudulentWithSameAddressAndDealId()
        {
            // Arrange
            Address address = Address.New("city", State.New("state"), Street.New("street"), "zipCode");
            Email email1 = Email.New("email1@domain.com");
            Email email2 = Email.New("email2@domain.com");
            Order order = new(address, creditCard: "creditCard", dealId: 1, email1, orderId: 1);
            Order anotherOrder = new(address, creditCard: "anotherCreditCard", dealId: 1, email2, orderId: 2);

            // Act
            bool isFraudulent = order.IsFraudulent(anotherOrder);

            // Assert
            isFraudulent.Should().BeTrue();
        }
    }
}
