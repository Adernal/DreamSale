//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Denmakers.DreamSale.Model.Customers;
//using Denmakers.DreamSale.Model.Orders;
//using Denmakers.DreamSale.Services.Configuration;
//using Denmakers.DreamSale.Model.Payments;
//using Denmakers.DreamSale.Common;
//using Denmakers.DreamSale.Services.Helpers;

//namespace Denmakers.DreamSale.Services.Payments
//{
//    public partial class PaymentService : IPaymentService
//    {
//        #region Fields
//        private readonly PaymentSettings _paymentSettings;
//        private readonly ISettingService _settingService;
//        private readonly ShoppingCartSettings _shoppingCartSettings;

//        #endregion

//        #region Ctor
//        public PaymentService(ISettingService settingService)
//        {
//            this._settingService = settingService;
//            this._paymentSettings = _settingService.LoadSetting<PaymentSettings>();
//            this._shoppingCartSettings = _settingService.LoadSetting<ShoppingCartSettings>();
//        }

//        #endregion

//        #region Methods

//        #region Processing

//        /// <summary>
//        /// Process a payment
//        /// </summary>
//        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
//        /// <returns>Process payment result</returns>
//        public virtual ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
//        {
//            if (processPaymentRequest.OrderTotal == decimal.Zero)
//            {
//                var result = new ProcessPaymentResult
//                {
//                    NewPaymentStatus = PaymentStatus.Paid
//                };
//                return result;
//            }

//            //We should strip out any white space or dash in the CC number entered.
//            if (!String.IsNullOrWhiteSpace(processPaymentRequest.CreditCardNumber))
//            {
//                processPaymentRequest.CreditCardNumber = processPaymentRequest.CreditCardNumber.Replace(" ", "");
//                processPaymentRequest.CreditCardNumber = processPaymentRequest.CreditCardNumber.Replace("-", "");
//            }
//            var paymentMethod = LoadPaymentMethodBySystemName(processPaymentRequest.PaymentMethodSystemName);
//            if (paymentMethod == null)
//                throw new DreamSaleException("Payment method couldn't be loaded");
//            return paymentMethod.ProcessPayment(processPaymentRequest);
//        }

//        /// <summary>
//        /// Post process payment (used by payment gateways that require redirecting to a third-party URL)
//        /// </summary>
//        /// <param name="postProcessPaymentRequest">Payment info required for an order processing</param>
//        public virtual void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
//        {
//            //already paid or order.OrderTotal == decimal.Zero
//            if (postProcessPaymentRequest.Order.PaymentStatus == PaymentStatus.Paid)
//                return;

//            var paymentMethod = LoadPaymentMethodBySystemName(postProcessPaymentRequest.Order.PaymentMethodSystemName);
//            if (paymentMethod == null)
//                throw new DreamSaleException("Payment method couldn't be loaded");
//            paymentMethod.PostProcessPayment(postProcessPaymentRequest);
//        }

//        /// <summary>
//        /// Gets a value indicating whether customers can complete a payment after order is placed but not completed (for redirection payment methods)
//        /// </summary>
//        /// <param name="order">Order</param>
//        /// <returns>Result</returns>
//        public virtual bool CanRePostProcessPayment(Order order)
//        {
//            if (order == null)
//                throw new ArgumentNullException("order");

//            if (!_paymentSettings.AllowRePostingPayments)
//                return false;

//            var paymentMethod = LoadPaymentMethodBySystemName(order.PaymentMethodSystemName);
//            if (paymentMethod == null)
//                return false; //Payment method couldn't be loaded (for example, was uninstalled)

//            if (paymentMethod.PaymentMethodType != PaymentMethodType.Redirection)
//                return false;   //this option is available only for redirection payment methods

//            if (order.Deleted)
//                return false;  //do not allow for deleted orders

//            if (order.OrderStatus == OrderStatus.Cancelled)
//                return false;  //do not allow for cancelled orders

//            if (order.PaymentStatus != PaymentStatus.Pending)
//                return false;  //payment status should be Pending

//            return paymentMethod.CanRePostProcessPayment(order);
//        }

//        /// <summary>
//        /// Gets an additional handling fee of a payment method
//        /// </summary>
//        /// <param name="cart">Shoping cart</param>
//        /// <param name="paymentMethodSystemName">Payment method system name</param>
//        /// <returns>Additional handling fee</returns>
//        public virtual decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart, string paymentMethodSystemName)
//        {
//            if (String.IsNullOrEmpty(paymentMethodSystemName))
//                return decimal.Zero;

//            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
//            if (paymentMethod == null)
//                return decimal.Zero;

//            decimal result = paymentMethod.GetAdditionalHandlingFee(cart);
//            if (result < decimal.Zero)
//                result = decimal.Zero;
//            if (_shoppingCartSettings.RoundPricesDuringCalculation)
//            {
//                result = RoundingHelper.RoundPrice(result);
//            }
//            return result;
//        }

//        /// <summary>
//        /// Gets a value indicating whether capture is supported by payment method
//        /// </summary>
//        /// <param name="paymentMethodSystemName">Payment method system name</param>
//        /// <returns>A value indicating whether capture is supported</returns>
//        public virtual bool SupportCapture(string paymentMethodSystemName)
//        {
//            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
//            if (paymentMethod == null)
//                return false;
//            return paymentMethod.SupportCapture;
//        }

//        /// <summary>
//        /// Captures payment
//        /// </summary>
//        /// <param name="capturePaymentRequest">Capture payment request</param>
//        /// <returns>Capture payment result</returns>
//        public virtual CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
//        {
//            var paymentMethod = LoadPaymentMethodBySystemName(capturePaymentRequest.Order.PaymentMethodSystemName);
//            if (paymentMethod == null)
//                throw new DreamSaleException("Payment method couldn't be loaded");
//            return paymentMethod.Capture(capturePaymentRequest);
//        }

//        /// <summary>
//        /// Gets a value indicating whether partial refund is supported by payment method
//        /// </summary>
//        /// <param name="paymentMethodSystemName">Payment method system name</param>
//        /// <returns>A value indicating whether partial refund is supported</returns>
//        public virtual bool SupportPartiallyRefund(string paymentMethodSystemName)
//        {
//            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
//            if (paymentMethod == null)
//                return false;
//            return paymentMethod.SupportPartiallyRefund;
//        }

//        /// <summary>
//        /// Gets a value indicating whether refund is supported by payment method
//        /// </summary>
//        /// <param name="paymentMethodSystemName">Payment method system name</param>
//        /// <returns>A value indicating whether refund is supported</returns>
//        public virtual bool SupportRefund(string paymentMethodSystemName)
//        {
//            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
//            if (paymentMethod == null)
//                return false;
//            return paymentMethod.SupportRefund;
//        }

//        /// <summary>
//        /// Refunds a payment
//        /// </summary>
//        /// <param name="refundPaymentRequest">Request</param>
//        /// <returns>Result</returns>
//        public virtual RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
//        {
//            var paymentMethod = LoadPaymentMethodBySystemName(refundPaymentRequest.Order.PaymentMethodSystemName);
//            if (paymentMethod == null)
//                throw new DreamSaleException("Payment method couldn't be loaded");
//            return paymentMethod.Refund(refundPaymentRequest);
//        }

//        /// <summary>
//        /// Gets a value indicating whether void is supported by payment method
//        /// </summary>
//        /// <param name="paymentMethodSystemName">Payment method system name</param>
//        /// <returns>A value indicating whether void is supported</returns>
//        public virtual bool SupportVoid(string paymentMethodSystemName)
//        {
//            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
//            if (paymentMethod == null)
//                return false;
//            return paymentMethod.SupportVoid;
//        }

//        /// <summary>
//        /// Voids a payment
//        /// </summary>
//        /// <param name="voidPaymentRequest">Request</param>
//        /// <returns>Result</returns>
//        public virtual VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
//        {
//            var paymentMethod = LoadPaymentMethodBySystemName(voidPaymentRequest.Order.PaymentMethodSystemName);
//            if (paymentMethod == null)
//                throw new DreamSaleException("Payment method couldn't be loaded");
//            return paymentMethod.Void(voidPaymentRequest);
//        }

//        /// <summary>
//        /// Gets a recurring payment type of payment method
//        /// </summary>
//        /// <param name="paymentMethodSystemName">Payment method system name</param>
//        /// <returns>A recurring payment type of payment method</returns>
//        public virtual RecurringPaymentType GetRecurringPaymentType(string paymentMethodSystemName)
//        {
//            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
//            if (paymentMethod == null)
//                return RecurringPaymentType.NotSupported;
//            return paymentMethod.RecurringPaymentType;
//        }

//        /// <summary>
//        /// Process recurring payment
//        /// </summary>
//        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
//        /// <returns>Process payment result</returns>
//        public virtual ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
//        {
//            if (processPaymentRequest.OrderTotal == decimal.Zero)
//            {
//                var result = new ProcessPaymentResult
//                {
//                    NewPaymentStatus = PaymentStatus.Paid
//                };
//                return result;
//            }

//            var paymentMethod = LoadPaymentMethodBySystemName(processPaymentRequest.PaymentMethodSystemName);
//            if (paymentMethod == null)
//                throw new DreamSaleException("Payment method couldn't be loaded");
//            return paymentMethod.ProcessRecurringPayment(processPaymentRequest);
//        }

//        /// <summary>
//        /// Cancels a recurring payment
//        /// </summary>
//        /// <param name="cancelPaymentRequest">Request</param>
//        /// <returns>Result</returns>
//        public virtual CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
//        {
//            if (cancelPaymentRequest.Order.OrderTotal == decimal.Zero)
//                return new CancelRecurringPaymentResult();

//            var paymentMethod = LoadPaymentMethodBySystemName(cancelPaymentRequest.Order.PaymentMethodSystemName);
//            if (paymentMethod == null)
//                throw new DreamSaleException("Payment method couldn't be loaded");
//            return paymentMethod.CancelRecurringPayment(cancelPaymentRequest);
//        }

//        /// <summary>
//        /// Gets masked credit card number
//        /// </summary>
//        /// <param name="creditCardNumber">Credit card number</param>
//        /// <returns>Masked credit card number</returns>
//        public virtual string GetMaskedCreditCardNumber(string creditCardNumber)
//        {
//            if (String.IsNullOrEmpty(creditCardNumber))
//                return string.Empty;

//            if (creditCardNumber.Length <= 4)
//                return creditCardNumber;

//            string last4 = creditCardNumber.Substring(creditCardNumber.Length - 4, 4);
//            string maskedChars = string.Empty;
//            for (int i = 0; i < creditCardNumber.Length - 4; i++)
//            {
//                maskedChars += "*";
//            }
//            return maskedChars + last4;
//        }

//        #endregion

//        #endregion
//    }
//}
