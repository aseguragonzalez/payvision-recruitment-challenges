using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Domain.ValueObjects;

namespace Refactoring.FraudDetection.Tests.Domain.ValueObjects
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void ShouldFailsWhenCreatingWithMissingValue()
        {
            // Arrange & Act
            Action act = () => _ = Email.New(null!);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'value')");
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        public void ShouldFailsWhenCreatingWithEmptyValue(string value)
        {
            // Arrange & Act
            Action act = () => _ = Email.New(value);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("The value cannot be an empty string or composed entirely of whitespace. (Parameter 'value')");
        }

        [TestMethod]
        public void ShouldFailsWhenCreatingWithInvalidValue()
        {
            // Arrange & Act
            Action act = () => _ = Email.New("invalid-email");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("The value is not a valid email address. (Parameter 'value')");
        }

        [TestMethod]
        [DataRow("name@domain.com", "name@domain.com")]
        [DataRow("name.surname@domain.com", "namesurname@domain.com")]
        [DataRow("name+v1@domain.com", "name@domain.com")]
        [DataRow("name.surname+v1@domain.com", "namesurname@domain.com")]
        public void ShouldCleanEmailValue(string value, string expected)
        {
            // Arrange & Act
            var email = Email.New(value);

            // Assert
            email.Should().Be(expected);
        }

        [TestMethod]
        public void ShouldBeEqual()
        {
            // Arrange
            Email email1 = Email.New("name@domain.com");
            Email email2 = Email.New("name@domain.com");

            // Act & Assert
            email1.Should().Be(email2);
        }

        [TestMethod]
        public void ShouldNotBeEqual()
        {
            // Arrange
            Email email1 = Email.New("name2@domain.com");
            Email email2 = Email.New("other@domain.com");

            // Act & Assert
            email1.Should().NotBe(email2);
        }

        [TestMethod]
        public void ShouldHaveSameHashCode()
        {
            // Arrange
            Email email1 = Email.New("name@domain.com");
            Email email2 = Email.New("name@domain.com");

            // Act & Assert
            email1.GetHashCode().Should().Be(email2.GetHashCode());
        }

        [TestMethod]
        public void ShouldHaveDifferentHashCode()
        {
            // Arrange
            Email email1 = Email.New("name@domain.com");
            Email email2 = Email.New("name2@domain.com");

            // Act & Assert
            email1.GetHashCode().Should().NotBe(email2.GetHashCode());
        }

    }
}
