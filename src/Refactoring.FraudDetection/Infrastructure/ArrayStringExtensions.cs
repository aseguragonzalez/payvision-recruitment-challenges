// <copyright file="ArrayStringExtensions.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection.Infrastructure
{
    using System.Globalization;

    public static class ArrayStringExtensions
    {
        public static Order MapOrderLineToOrder(this string[] items)
        {
            ArgumentNullException.ThrowIfNull(items, nameof(items));
            return new Order
            {
                OrderId = int.Parse(items[0], CultureInfo.InvariantCulture),
                DealId = int.Parse(items[1], CultureInfo.InvariantCulture),
                Email = items[2].ToUpperInvariant(),
                Street = items[3].ToUpperInvariant(),
                City = items[4].ToUpperInvariant(),
                State = items[5].ToUpperInvariant(),
                ZipCode = items[6],
                CreditCard = items[7]
            };
        }
    }
}
