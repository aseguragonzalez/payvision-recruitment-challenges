using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Domain.ValueObjects;

namespace Refactoring.FraudDetection.Tests.Domain.ValueObjects
{
    [TestClass]
    public class StreetTests
    {
        [TestMethod]
        public void ShouldFailsWhenCreatingWithNullValue()
        {
            // Arrange
            Action action = () => _ = Street.New(null!);

            // Act & Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'value')");
        }


        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        public void ShouldFailsWhenCreatingWithEmptylValue(string value)
        {
            // Arrange & Act
            Action act = () => _ = Street.New(value);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'value')");
        }

        [TestMethod]
        public void ShouldNormalizeToUpperValue()
        {
            // Arrange & Act
            Street street = Street.New("any VALUE");

            // Assert
            street.Value.Should().Be("ANY VALUE");
        }

        [TestMethod]
        public void ShouldNormalizeValueWhenItContainsStreet()
        {
            // Arrange & Act
            Street street = Street.New("st.");

            // Assert
            street.Value.Should().Contain("STREET");
        }

        [TestMethod]
        public void ShouldNormalizeValueWhenItContainsRoad()
        {
            // Arrange & Act
            Street street = Street.New("rd.");

            // Assert
            street.Value.Should().Be("ROAD");
        }

        [TestMethod]
        public void ShouldBeEqualWhenValuesAreEqual()
        {
            // Arrange
            Street street1 = Street.New("any value");
            Street street2 = Street.New("ANY value");

            // Act & Assert
            street1.Should().Be(street2);
        }

        [TestMethod]
        public void ShouldNotBeEqualWhenValuesAreDifferent()
        {
            // Arrange
            Street street1 = Street.New("any value");
            Street street2 = Street.New("other value");

            // Act & Assert
            street1.Should().NotBe(street2);
        }

        [TestMethod]
        public void ShouldHaveSameHashCodeWhenValuesAreEqual()
        {
            // Arrange
            Street street1 = Street.New("any value");
            Street street2 = Street.New("ANY value");

            // Act & Assert
            street1.GetHashCode().Should().Be(street2.GetHashCode());
        }

        [TestMethod]
        public void ShouldHaveDifferentHashCodeWhenValuesAreDifferent()
        {
            // Arrange
            Street street1 = Street.New("any value");
            Street street2 = Street.New("other value");

            // Act & Assert
            street1.GetHashCode().Should().NotBe(street2.GetHashCode());
        }

        [TestMethod]
        public void ShouldReturnTheValueWhenCallingToString()
        {
            // Arrange
            Street street = Street.New("Any Value");

            // Act
            string result = street.ToString();

            // Assert
            result.Should().Be("ANY VALUE");
        }

    }
}
