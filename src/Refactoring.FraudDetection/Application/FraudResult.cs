// <copyright file="FraudResult.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection.Application
{
    public sealed class FraudResult
    {
        public int OrderId { get; }

        public bool IsFraudulent { get; }

        public FraudResult(int orderId, bool isFraudulent)
        {
            OrderId = orderId;
            IsFraudulent = isFraudulent;
        }

        public FraudResult(int orderId) : this(orderId, true)
        {
        }
    }
}
