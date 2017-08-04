
using Denmakers.DreamSale.Model.Orders;

namespace Denmakers.DreamSale.Services.Payments
{
    public partial class CancelRecurringPaymentRequest
    {
        /// <summary>
        /// Gets or sets an order
        /// </summary>
        public Order Order { get; set; }
    }
}
