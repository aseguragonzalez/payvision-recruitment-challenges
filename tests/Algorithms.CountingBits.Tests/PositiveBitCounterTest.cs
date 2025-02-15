// <copyright file="PositiveBitCounterTest.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Algorithms.CountingBits.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PositiveBitCounterTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CountNegativeValueArgumentExceptionExpected()
        {
            PositiveBitCounter.Count(-2);
        }

        [TestMethod]
        public void CountZeroNoOccurrences()
        {
            CollectionAssert.AreEqual(
                expected: new List<int> { 0 },
                actual: PositiveBitCounter.Count(0).ToList(),
                message: "The result is not the expected");
        }

        [TestMethod]
        public void CountValidInputOneOcurrence()
        {
            CollectionAssert.AreEqual(
                expected: new List<int> { 1, 0 },
                actual: PositiveBitCounter.Count(1).ToList(),
                message: "The result is not the expected");
        }

        [TestMethod]
        public void CountValidInputMultipleOcurrence()
        {
            CollectionAssert.AreEqual(
                expected: new List<int> { 3, 0, 5, 7 },
                actual: PositiveBitCounter.Count(161).ToList(),
                message: "The result is not the expected");
        }
    }
}
