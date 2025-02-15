using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Domain.ValueObjects;

namespace Refactoring.FraudDetection.Tests.Domain.ValueObjects
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void ShouldCreateAnInstanceOfAddress()
        {
            // Arrange & act
            Address address = Address.New("new-value", State.New("new-value"), Street.New("new-value"), "new-value");

            // Assert
            address.Should().NotBeNull();
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void ShouldFailsWhenCityIsInvalidOrMissing(string value)
        {
            // Arrange & act
            Action act = () => _ = Address.New(value, State.New("new-value"), Street.New("new-value"), "new-value");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("City is invalid or missing.");
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void ShouldFailsWhenZipCodeIsInvalidOrMissing(string value)
        {
            // Arrange & act
            Action act = () => _ = Address.New("new-value", State.New("new-value"), Street.New("new-value"), value);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("ZipCode is invalid or missing.");
        }

        [TestMethod]
        public void ShouldFailsWhenStateIsNull()
        {
            // Arrange & act
            Action act = () => _ = Address.New("new-value", null!, Street.New("new-value"), "new-value");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Value cannot be null. (Parameter 'state')");
        }

        [TestMethod]
        public void ShouldFailsWhenStreetIsNull()
        {
            // Arrange & act
            Action act = () => _ = Address.New("new-value", State.New("new-value"), null!, "new-value");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("Value cannot be null. (Parameter 'street')");
        }


        [TestMethod]
        public void ShouldBeEqual()
        {
            // Arrange & act
            Address address1 = Address.New("new-value", State.New("new-value"), Street.New("new-value"), "new-value");
            Address address2 = Address.New("new-value", State.New("new-value"), Street.New("new-value"), "new-value");

            // Assert
            address1.Should().Be(address2);
        }

        [TestMethod]
        [DataRow("new-value1", "new-value", "new-value", "new-value")]
        [DataRow("new-value", "new-value1", "new-value", "new-value")]
        [DataRow("new-value", "new-value", "new-value1", "new-value")]
        [DataRow("new-value", "new-value", "new-value", "new-value1")]
        public void ShouldNotBeEqual(string city, string state, string street, string zipCode)
        {
            // Arrange & act
            Address address1 = Address.New("new-value", State.New("new-value"), Street.New("new-value"), "new-value");
            Address address2 = Address.New(city, State.New(state), Street.New(street), zipCode);

            // Assert
            address1.Should().NotBe(address2);
        }

        [TestMethod]
        public void ShouldHaveSameHashCode()
        {
            // Arrange & act
            Address address1 = Address.New("new-value", State.New("new-value"), Street.New("new-value"), "new-value");
            Address address2 = Address.New("new-value", State.New("new-value"), Street.New("new-value"), "new-value");

            // Assert
            address1.GetHashCode().Should().Be(address2.GetHashCode());
        }

        [TestMethod]
        [DataRow("new-value1", "new-value", "new-value", "new-value")]
        [DataRow("new-value", "new-value1", "new-value", "new-value")]
        [DataRow("new-value", "new-value", "new-value1", "new-value")]
        [DataRow("new-value", "new-value", "new-value", "new-value1")]
        public void ShouldHaveDifferentHashCode(string city, string state, string street, string zipCode)
        {
            // Arrange & act
            Address address1 = Address.New("new-value", State.New("new-value"), Street.New("new-value"), "new-value");
            Address address2 = Address.New(city, State.New(state), Street.New(street), zipCode);

            // Assert
            address1.GetHashCode().Should().NotBe(address2.GetHashCode());
        }
    }
}
