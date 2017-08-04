using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Orders
{
    public partial class OrderAverageReportLineSummaryVM
    {
        [DisplayName("Admin.SalesReport.Average.OrderStatus")]
        public string OrderStatus { get; set; }

        [DisplayName("Admin.SalesReport.Average.SumTodayOrders")]
        public string SumTodayOrders { get; set; }

        [DisplayName("Admin.SalesReport.Average.SumThisWeekOrders")]
        public string SumThisWeekOrders { get; set; }

        [DisplayName("Admin.SalesReport.Average.SumThisMonthOrders")]
        public string SumThisMonthOrders { get; set; }

        [DisplayName("Admin.SalesReport.Average.SumThisYearOrders")]
        public string SumThisYearOrders { get; set; }

        [DisplayName("Admin.SalesReport.Average.SumAllTimeOrders")]
        public string SumAllTimeOrders { get; set; }
    }
}
