using Denmakers.DreamSale.Model.Orders;

namespace Denmakers.DreamSale.Services.Orders
{
    public partial interface ICustomNumberFormatter
    {
        string GenerateReturnRequestCustomNumber(ReturnRequest returnRequest);

        string GenerateOrderCustomNumber(Order order);
    }
}
