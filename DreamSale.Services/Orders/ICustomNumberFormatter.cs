using DreamSale.Model.Orders;

namespace DreamSale.Services.Orders
{
    public partial interface ICustomNumberFormatter
    {
        string GenerateReturnRequestCustomNumber(ReturnRequest returnRequest);

        string GenerateOrderCustomNumber(Order order);
    }
}