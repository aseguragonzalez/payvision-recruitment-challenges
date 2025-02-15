// <copyright file="FraudRadar.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection.Application
{
    using System.Collections.Generic;
    using Refactoring.FraudDetection.Domain;
    using Refactoring.FraudDetection.Domain.Entities;
    using Refactoring.FraudDetection.Domain.ValueObjects;

    public sealed class FraudRadar
    {
        private readonly IOrdersReportRepository ordersReportRepository;

        public FraudRadar(IOrdersReportRepository ordersReportRepository)
        {
            ArgumentNullException.ThrowIfNull(ordersReportRepository, nameof(ordersReportRepository));

            this.ordersReportRepository = ordersReportRepository;
        }

        public IEnumerable<FraudResult> Check(OrdersReportId ordersReportId)
        {
            // READ FRAUD LINES
            OrdersReport ordersReport = ordersReportRepository.GetOrdersReport(ordersReportId);

            // CHECK FRAUD
            return ordersReport.GetFraudulentOrders().Select(ToFraudResult);
        }

        private static FraudResult ToFraudResult(Order order) => new(order.OrderId);
    }
}
