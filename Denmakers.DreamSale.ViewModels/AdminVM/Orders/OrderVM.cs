using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Tax;
using Denmakers.DreamSale.ViewModels.AdminVM.Addresses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Orders
{
    public partial class OrderVM
    {
        public OrderVM()
        {
            CustomValues = new Dictionary<string, object>();
            TaxRates = new List<TaxRate>();
            GiftCards = new List<GiftCard>();
            Items = new List<OrderItemVM>();
            UsedDiscounts = new List<UsedDiscountVM>();
            Warnings = new List<string>();
            CustomProperties = new Dictionary<string, object>();
        }


        public bool IsLoggedInAsVendor { get; set; }

        //identifiers
        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Order GUID")]
        public Guid OrderGuid { get; set; }
        [DisplayName("Admin.Orders.Fields.CustomOrderNumber")]
        public string CustomOrderNumber { get; set; }

        //store
        [DisplayName("Admin.Orders.Fields.Store")]
        public string StoreName { get; set; }

        //customer info
        [DisplayName("Admin.Orders.Fields.Customer")]
        public int CustomerId { get; set; }
        [DisplayName("Admin.Orders.Fields.Customer")]
        public string CustomerInfo { get; set; }
        [DisplayName("Admin.Orders.Fields.CustomerEmail")]
        public string CustomerEmail { get; set; }
        public string CustomerFullName { get; set; }
        [DisplayName("Admin.Orders.Fields.CustomerIP")]
        public string CustomerIp { get; set; }

        [DisplayName("Admin.Orders.Fields.CustomValues")]
        public Dictionary<string, object> CustomValues { get; set; }

        [DisplayName("Admin.Orders.Fields.Affiliate")]
        public int AffiliateId { get; set; }
        [DisplayName("Admin.Orders.Fields.Affiliate")]
        public string AffiliateName { get; set; }

        //Used discounts
        [DisplayName("Admin.Orders.Fields.UsedDiscounts")]
        public IList<UsedDiscountVM> UsedDiscounts { get; set; }

        //totals
        public bool AllowCustomersToSelectTaxDisplayType { get; set; }
        public TaxDisplayType TaxDisplayType { get; set; }
        [DisplayName("Admin.Orders.Fields.OrderSubtotalInclTax")]
        public string OrderSubtotalInclTax { get; set; }
        [DisplayName("Admin.Orders.Fields.OrderSubtotalExclTax")]
        public string OrderSubtotalExclTax { get; set; }
        [DisplayName("Admin.Orders.Fields.OrderSubTotalDiscountInclTax")]
        public string OrderSubTotalDiscountInclTax { get; set; }
        [DisplayName("Admin.Orders.Fields.OrderSubTotalDiscountExclTax")]
        public string OrderSubTotalDiscountExclTax { get; set; }
        [DisplayName("Admin.Orders.Fields.OrderShippingInclTax")]
        public string OrderShippingInclTax { get; set; }
        [DisplayName("Admin.Orders.Fields.OrderShippingExclTax")]
        public string OrderShippingExclTax { get; set; }
        [DisplayName("Admin.Orders.Fields.PaymentMethodAdditionalFeeInclTax")]
        public string PaymentMethodAdditionalFeeInclTax { get; set; }
        [DisplayName("Admin.Orders.Fields.PaymentMethodAdditionalFeeExclTax")]
        public string PaymentMethodAdditionalFeeExclTax { get; set; }
        [DisplayName("Admin.Orders.Fields.Tax")]
        public string Tax { get; set; }
        public IList<TaxRate> TaxRates { get; set; }
        public bool DisplayTax { get; set; }
        public bool DisplayTaxRates { get; set; }
        [DisplayName("Admin.Orders.Fields.OrderTotalDiscount")]
        public string OrderTotalDiscount { get; set; }
        [DisplayName("Admin.Orders.Fields.RedeemedRewardPoints")]
        public int RedeemedRewardPoints { get; set; }
        [DisplayName("Admin.Orders.Fields.RedeemedRewardPoints")]
        public string RedeemedRewardPointsAmount { get; set; }
        [DisplayName("Admin.Orders.Fields.OrderTotal")]
        public string OrderTotal { get; set; }
        [DisplayName("Admin.Orders.Fields.RefundedAmount")]
        public string RefundedAmount { get; set; }
        [DisplayName("Admin.Orders.Fields.Profit")]
        public string Profit { get; set; }

        //edit totals
        [DisplayName("Admin.Orders.Fields.Edit.OrderSubtotal")]
        public decimal OrderSubtotalInclTaxValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.OrderSubtotal")]
        public decimal OrderSubtotalExclTaxValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.OrderSubTotalDiscount")]
        public decimal OrderSubTotalDiscountInclTaxValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.OrderSubTotalDiscount")]
        public decimal OrderSubTotalDiscountExclTaxValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.OrderShipping")]
        public decimal OrderShippingInclTaxValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.OrderShipping")]
        public decimal OrderShippingExclTaxValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.PaymentMethodAdditionalFee")]
        public decimal PaymentMethodAdditionalFeeInclTaxValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.PaymentMethodAdditionalFee")]
        public decimal PaymentMethodAdditionalFeeExclTaxValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.Tax")]
        public decimal TaxValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.TaxRates")]
        public string TaxRatesValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.OrderTotalDiscount")]
        public decimal OrderTotalDiscountValue { get; set; }
        [DisplayName("Admin.Orders.Fields.Edit.OrderTotal")]
        public decimal OrderTotalValue { get; set; }

        //associated recurring payment id
        [DisplayName("Admin.Orders.Fields.RecurringPayment")]
        public int RecurringPaymentId { get; set; }

        //order status
        [DisplayName("Admin.Orders.Fields.OrderStatus")]
        public string OrderStatus { get; set; }
        [DisplayName("Admin.Orders.Fields.OrderStatus")]
        public int OrderStatusId { get; set; }

        //payment info
        [DisplayName("Admin.Orders.Fields.PaymentStatus")]
        public string PaymentStatus { get; set; }
        [DisplayName("Admin.Orders.Fields.PaymentStatus")]
        public int PaymentStatusId { get; set; }
        [DisplayName("Admin.Orders.Fields.PaymentMethod")]
        public string PaymentMethod { get; set; }

        //credit card info
        public bool AllowStoringCreditCardNumber { get; set; }
        [DisplayName("Admin.Orders.Fields.CardType")]
        [AllowHtml]
        public string CardType { get; set; }
        [DisplayName("Admin.Orders.Fields.CardName")]
        [AllowHtml]
        public string CardName { get; set; }
        [DisplayName("Admin.Orders.Fields.CardNumber")]
        [AllowHtml]
        public string CardNumber { get; set; }
        [DisplayName("Admin.Orders.Fields.CardCVV2")]
        [AllowHtml]
        public string CardCvv2 { get; set; }
        [DisplayName("Admin.Orders.Fields.CardExpirationMonth")]
        [AllowHtml]
        public string CardExpirationMonth { get; set; }
        [DisplayName("Admin.Orders.Fields.CardExpirationYear")]
        [AllowHtml]
        public string CardExpirationYear { get; set; }

        //misc payment info
        [DisplayName("Admin.Orders.Fields.AuthorizationTransactionID")]
        public string AuthorizationTransactionId { get; set; }
        [DisplayName("Admin.Orders.Fields.CaptureTransactionID")]
        public string CaptureTransactionId { get; set; }
        [DisplayName("Admin.Orders.Fields.SubscriptionTransactionID")]
        public string SubscriptionTransactionId { get; set; }

        //shipping info
        public bool IsShippable { get; set; }
        public bool PickUpInStore { get; set; }
        [DisplayName("Admin.Orders.Fields.PickupAddress")]
        public AddressVM PickupAddress { get; set; }
        public string PickupAddressGoogleMapsUrl { get; set; }
        [DisplayName("Admin.Orders.Fields.ShippingStatus")]
        public string ShippingStatus { get; set; }
        [DisplayName("Admin.Orders.Fields.ShippingStatus")]
        public int ShippingStatusId { get; set; }
        [DisplayName("Admin.Orders.Fields.ShippingAddress")]
        public AddressVM ShippingAddress { get; set; }
        [DisplayName("Admin.Orders.Fields.ShippingMethod")]
        public string ShippingMethod { get; set; }
        public string ShippingAddressGoogleMapsUrl { get; set; }
        public bool CanAddNewShipments { get; set; }

        //billing info
        [DisplayName("Admin.Orders.Fields.BillingAddress")]
        public AddressVM BillingAddress { get; set; }
        [DisplayName("Admin.Orders.Fields.VatNumber")]
        public string VatNumber { get; set; }

        //gift cards
        public IList<GiftCard> GiftCards { get; set; }

        //items
        public bool HasDownloadableProducts { get; set; }
        public IList<OrderItemVM> Items { get; set; }

        //creation date
        [DisplayName("Admin.Orders.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        //checkout attributes
        public string CheckoutAttributeInfo { get; set; }


        //order notes
        [DisplayName("Admin.Orders.OrderNotes.Fields.DisplayToCustomer")]
        public bool AddOrderNoteDisplayToCustomer { get; set; }
        [DisplayName("Admin.Orders.OrderNotes.Fields.Note")]
        [AllowHtml]
        public string AddOrderNoteMessage { get; set; }
        public bool AddOrderNoteHasDownload { get; set; }
        [DisplayName("Admin.Orders.OrderNotes.Fields.Download")]
        [UIHint("Download")]
        public int AddOrderNoteDownloadId { get; set; }

        //refund info
        [DisplayName("Admin.Orders.Fields.PartialRefund.AmountToRefund")]
        public decimal AmountToRefund { get; set; }
        public decimal MaxAmountToRefund { get; set; }
        public string PrimaryStoreCurrencyCode { get; set; }

        //workflow info
        public bool CanCancelOrder { get; set; }
        public bool CanCapture { get; set; }
        public bool CanMarkOrderAsPaid { get; set; }
        public bool CanRefund { get; set; }
        public bool CanRefundOffline { get; set; }
        public bool CanPartiallyRefund { get; set; }
        public bool CanPartiallyRefundOffline { get; set; }
        public bool CanVoid { get; set; }
        public bool CanVoidOffline { get; set; }

        //warnings
        public List<string> Warnings { get; set; }

        #region Nested Classes

        public partial class OrderItemVM
        {
            public OrderItemVM()
            {
                PurchasedGiftCardIds = new List<int>();
                ReturnRequests = new List<ReturnRequestBriefVM>();
                CustomProperties = new Dictionary<string, object>();
            }
            public int Id { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string VendorName { get; set; }
            public string Sku { get; set; }

            public string PictureThumbnailUrl { get; set; }

            public string UnitPriceInclTax { get; set; }
            public string UnitPriceExclTax { get; set; }
            public decimal UnitPriceInclTaxValue { get; set; }
            public decimal UnitPriceExclTaxValue { get; set; }

            public int Quantity { get; set; }

            public string DiscountInclTax { get; set; }
            public string DiscountExclTax { get; set; }
            public decimal DiscountInclTaxValue { get; set; }
            public decimal DiscountExclTaxValue { get; set; }

            public string SubTotalInclTax { get; set; }
            public string SubTotalExclTax { get; set; }
            public decimal SubTotalInclTaxValue { get; set; }
            public decimal SubTotalExclTaxValue { get; set; }

            public string AttributeInfo { get; set; }
            public string RecurringInfo { get; set; }
            public string RentalInfo { get; set; }
            public IList<ReturnRequestBriefVM> ReturnRequests { get; set; }
            public IList<int> PurchasedGiftCardIds { get; set; }

            public bool IsDownload { get; set; }
            public int DownloadCount { get; set; }
            public DownloadActivationType DownloadActivationType { get; set; }
            public bool IsDownloadActivated { get; set; }
            public Guid LicenseDownloadGuid { get; set; }

            #region Nested Classes

            public partial class ReturnRequestBriefVM
            {
                public int Id { get; set; }
                public string CustomNumber { get; set; }
            }

            #endregion
        }

        public partial class TaxRate
        {
            public int Id { get; set; }
            public string Rate { get; set; }
            public string Value { get; set; }
        }

        public partial class GiftCard
        {
            public int Id { get; set; }
            [DisplayName("Admin.Orders.Fields.GiftCardInfo")]
            public string CouponCode { get; set; }
            public string Amount { get; set; }
        }

        public partial class OrderNote
        {
            public int Id { get; set; }
            public int OrderId { get; set; }
            [DisplayName("Admin.Orders.OrderNotes.Fields.DisplayToCustomer")]
            public bool DisplayToCustomer { get; set; }
            [DisplayName("Admin.Orders.OrderNotes.Fields.Note")]
            public string Note { get; set; }
            [DisplayName("Admin.Orders.OrderNotes.Fields.Download")]
            public int DownloadId { get; set; }
            [DisplayName("Admin.Orders.OrderNotes.Fields.Download")]
            public Guid DownloadGuid { get; set; }
            [DisplayName("Admin.Orders.OrderNotes.Fields.CreatedOn")]
            public DateTime CreatedOn { get; set; }
        }

        public partial class UploadLicenseVM
        {
            public int Id { get; set; }
            public int OrderId { get; set; }

            public int OrderItemId { get; set; }

            [UIHint("Download")]
            public int LicenseDownloadId { get; set; }

        }

        public partial class AddOrderProductVM
        {
            public AddOrderProductVM()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
                AvailableProductTypes = new List<SelectListItem>();
                CustomProperties = new Dictionary<string, object>();
            }

            public int Id { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }

            [DisplayName("Admin.Catalog.Products.List.SearchProductName")]
            [AllowHtml]
            public string SearchProductName { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchCategory")]
            public int SearchCategoryId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchManufacturer")]
            public int SearchManufacturerId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchProductType")]
            public int SearchProductTypeId { get; set; }

            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableManufacturers { get; set; }
            public IList<SelectListItem> AvailableProductTypes { get; set; }

            public int OrderId { get; set; }

            #region Nested classes

            public partial class ProductVM
            {
                public int Id { get; set; }

                [DisplayName("Admin.Orders.Products.AddNew.Name")]
                [AllowHtml]
                public string Name { get; set; }

                [DisplayName("Admin.Orders.Products.AddNew.SKU")]
                [AllowHtml]
                public string Sku { get; set; }
            }

            public partial class ProductDetailsVM
            {
                public ProductDetailsVM()
                {
                    ProductAttributes = new List<ProductAttributeVM>();
                    GiftCard = new GiftCardVM();
                    Warnings = new List<string>();
                }
                public int Id { get; set; }
                public int ProductId { get; set; }

                public int OrderId { get; set; }

                public ProductType ProductType { get; set; }

                public string Name { get; set; }

                [DisplayName("Admin.Orders.Products.AddNew.UnitPriceInclTax")]
                public decimal UnitPriceInclTax { get; set; }
                [DisplayName("Admin.Orders.Products.AddNew.UnitPriceExclTax")]
                public decimal UnitPriceExclTax { get; set; }

                [DisplayName("Admin.Orders.Products.AddNew.Quantity")]
                public int Quantity { get; set; }

                [DisplayName("Admin.Orders.Products.AddNew.SubTotalInclTax")]
                public decimal SubTotalInclTax { get; set; }
                [DisplayName("Admin.Orders.Products.AddNew.SubTotalExclTax")]
                public decimal SubTotalExclTax { get; set; }

                //product attributes
                public IList<ProductAttributeVM> ProductAttributes { get; set; }
                //gift card info
                public GiftCardVM GiftCard { get; set; }
                //rental
                public bool IsRental { get; set; }

                public List<string> Warnings { get; set; }

                /// <summary>
                /// A value indicating whether this attribute depends on some other attribute
                /// </summary>
                public bool HasCondition { get; set; }

                public bool AutoUpdateOrderTotals { get; set; }
            }

            public partial class ProductAttributeVM
            {
                public ProductAttributeVM()
                {
                    Values = new List<ProductAttributeValueVM>();
                }

                public int Id { get; set; }
                public int ProductAttributeId { get; set; }

                public string Name { get; set; }

                public string TextPrompt { get; set; }

                public bool IsRequired { get; set; }

                public bool HasCondition { get; set; }

                /// <summary>
                /// Allowed file extensions for customer uploaded files
                /// </summary>
                public IList<string> AllowedFileExtensions { get; set; }

                public AttributeControlType AttributeControlType { get; set; }

                public IList<ProductAttributeValueVM> Values { get; set; }
            }

            public partial class ProductAttributeValueVM
            {
                public int Id { get; set; }
                public string Name { get; set; }

                public bool IsPreSelected { get; set; }

                public string PriceAdjustment { get; set; }

                public decimal PriceAdjustmentValue { get; set; }

                public bool CustomerEntersQty { get; set; }

                public int Quantity { get; set; }
            }


            public partial class GiftCardVM
            {
                public int Id { get; set; }
                public bool IsGiftCard { get; set; }

                [DisplayName("Admin.GiftCards.Fields.RecipientName")]
                [AllowHtml]
                public string RecipientName { get; set; }
                [DisplayName("Admin.GiftCards.Fields.RecipientEmail")]
                [AllowHtml]
                public string RecipientEmail { get; set; }
                [DisplayName("Admin.GiftCards.Fields.SenderName")]
                [AllowHtml]
                public string SenderName { get; set; }
                [DisplayName("Admin.GiftCards.Fields.SenderEmail")]
                [AllowHtml]
                public string SenderEmail { get; set; }
                [DisplayName("Admin.GiftCards.Fields.Message")]
                [AllowHtml]
                public string Message { get; set; }

                public GiftCardType GiftCardType { get; set; }
            }
            #endregion
        }

        public partial class UsedDiscountVM
        {
            public int Id { get; set; }
            public int DiscountId { get; set; }
            public string DiscountName { get; set; }
        }

        #endregion
    }


    public partial class OrderAggreratorVM
    {
        public int Id { get; set; }
        //aggergator properties
        public string aggregatorprofit { get; set; }
        public string aggregatorshipping { get; set; }
        public string aggregatortax { get; set; }
        public string aggregatortotal { get; set; }
    }
}
