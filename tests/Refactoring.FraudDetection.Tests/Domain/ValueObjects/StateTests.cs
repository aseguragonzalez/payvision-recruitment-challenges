using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Domain.ValueObjects;

namespace Refactoring.FraudDetection.Tests.Domain.ValueObjects
{
    [TestClass]
    public class StateTests
    {
        [TestMethod]
        public void ShouldFailWhenValueIsNull()
        {
            // Arrange & Act
            Action act = () => _ = State.New(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'value')");
        }


        [TestMethod]
        public void ShouldBeNormlizedToUpper()
        {
            // Act & Arrange
            State state = State.New("custom");

            // Assert
            state.Value.Should().Be("CUSTOM");
        }

        [TestMethod]
        [DataRow("il", "ILLINOIS")]
        [DataRow("ca", "CALIFORNIA")]
        [DataRow("NY", "NEW YORK")]
        [DataTestMethod]
        public void StateShouldBeNormalized(string value, string expected)
        {
            // Arrange & Act
            State state = State.New(value);

            // Assert
            state.Value.Should().Be(expected);
        }

        [TestMethod]
        public void ShouldBeEqual()
        {
            // Arrange
            State state1 = State.New("IL");
            State state2 = State.New("il");

            // Act & Assert
            state1.Should().Be(state2);
        }

        [TestMethod]
        public void ShouldNotBeEqual()
        {
            // Arrange
            State state1 = State.New("IL");
            State state2 = State.New("NY");

            // Act & Assert
            state1.Should().NotBe(state2);
        }

        [TestMethod]
        public void ShouldHaveSameHashCode()
        {
            // Arrange
            State state1 = State.New("IL");
            State state2 = State.New("il");

            // Act & Assert
            state1.GetHashCode().Should().Be(state2.GetHashCode());
        }

        [TestMethod]
        public void ShouldHaveDifferentHashCode()
        {
            // Arrange
            State state1 = State.New("IL");
            State state2 = State.New("NY");

            // Act & Assert
            state1.GetHashCode().Should().NotBe(state2.GetHashCode());
        }

        [TestMethod]
        public void ShouldHaveSameToString()
        {
            // Arrange
            State state1 = State.New("IL");
            State state2 = State.New("il");

            // Act & Assert
            state1.ToString().Should().Be(state2.ToString());
        }
    }
}
