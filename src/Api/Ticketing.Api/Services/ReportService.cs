using Ticketing.Application.Reports;

namespace Ticketing.Application.Services;

public sealed class ReportService
{
    public SalesReportDto CreateSalesReport(SalesReportRequest request)
    {
        var days = request.To.DayNumber - request.From.DayNumber + 1;
        var ticketsSold = Math.Max(0, days) * 42;
        var revenue = ticketsSold * 37.5m;

        return new SalesReportDto(
            string.IsNullOrWhiteSpace(request.EventId) ? "global" : request.EventId,
            ticketsSold,
            revenue,
            Math.Max(1, ticketsSold / 3),
            ["Reporte inicial generado desde el esqueleto.", "Conectar agregaciones reales en MongoDB para producción."]);
    }
}
