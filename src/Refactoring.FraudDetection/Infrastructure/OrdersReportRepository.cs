// <copyright file="OrdersRepository.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Refactoring.FraudDetection.Domain;
    using Refactoring.FraudDetection.Domain.Entities;
    using Refactoring.FraudDetection.Domain.ValueObjects;

    public sealed class OrdersRepository : IOrdersReportRepository
    {
        public OrdersReport GetOrdersReport(OrdersReportId ordersReportId)
        {
            ArgumentNullException.ThrowIfNull(ordersReportId, nameof(ordersReportId));
            string filePath = Path.Combine(Environment.CurrentDirectory, "Files", $"{ordersReportId}");
            IEnumerable<Order> orders = File.ReadAllLines(filePath).Select(Orders);
            return new(ordersReportId, orders.Select(x => x.ToEntity()));
        }

        private static Order Orders(string orderLine) =>
            orderLine.Split([','], StringSplitOptions.RemoveEmptyEntries).MapOrderLineToOrder();
    }
}
