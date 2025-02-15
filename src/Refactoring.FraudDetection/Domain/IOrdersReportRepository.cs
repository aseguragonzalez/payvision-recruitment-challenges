// <copyright file="IOrdersRepository.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection.Domain
{
    using Refactoring.FraudDetection.Domain.Entities;
    using Refactoring.FraudDetection.Domain.ValueObjects;

    public interface IOrdersReportRepository
    {
        OrdersReport GetOrdersReport(OrdersReportId ordersReportId);
    }
}
