namespace Denmakers.DreamSale.Services.Payments.PaymentMethods.PayPalDirect
{
    public enum TransactMode
    {
        /// <summary>
        /// Authorize
        /// </summary>
        Authorize = 0,

        /// <summary>
        /// Authorize and capture
        /// </summary>
        AuthorizeAndCapture = 2
    }
}
