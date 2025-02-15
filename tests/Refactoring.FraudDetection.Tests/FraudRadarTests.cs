// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection.Tests
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Refactoring.FraudDetection.Application;
    using Refactoring.FraudDetection.Domain.ValueObjects;
    using Refactoring.FraudDetection.Infrastructure;

    [TestClass]
    public class FraudRadarTests
    {
        [TestMethod]
        [DeploymentItem("./Files/OneLineFile.txt", "Files")]
        public void ShouldNotDetectFraudWhenFileHasOneLine()
        {
            // Arrange & Act
            var result = ExecuteTest("OneLineFile.txt");

            // Assert
            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(0, "The result should not contains fraudulent lines");
        }

        [TestMethod]
        [DeploymentItem("./Files/TwoLines_FraudulentSecond.txt", "Files")]
        public void ShouldGetOneFraudulentOrderWhenSameAddress()
        {
            // Arrange & Act
            var result = ExecuteTest("TwoLines_FraudulentSecond.txt");

            // Assert
            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/ThreeLines_FraudulentSecond.txt", "Files")]
        public void ShouldGetOneFraudulentOrderWhenSameEmail()
        {
            // Arrange & Act
            var result = ExecuteTest("ThreeLines_FraudulentSecond.txt");

            // Assert
            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/FourLines_MoreThanOneFraudulent.txt", "Files")]
        public void ShouldGetMultipleFraudulentOrders()
        {
            // Arrange & Act
            var result = ExecuteTest("FourLines_MoreThanOneFraudulent.txt");

            // Assert
            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(2, "The result should contains the number of lines of the file");
        }

        private static List<FraudResult> ExecuteTest(string filename)
        {
            FraudRadar fraudRadar = new(new OrdersRepository());

            return fraudRadar.Check(OrdersReportId.New(filename)).ToList();
        }
    }
}
